using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WishList.ViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService iUserService)
        {
            userService = iUserService;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userService.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                if (user.EmailConfirmed == true)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Не подтвержден email.");
                    return View(model);
                }
            }

            ModelState.AddModelError("", "Invalid username or password.");

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private async Task SignInAsync(DomainUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await userService.GenerateClaimAsync(user));
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var file = Request.Files["file"];

            var avatarPath = "";

            if (file != null && file.ContentLength > 0)
            {
                avatarPath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(StringResources.UsersAvatarsPath), avatarPath);
                file.SaveAs(path);
            }
            else
            {
                avatarPath = StringResources.NoUserAvatarPath;
            }

            model.Avatar = StringResources.UsersAvatarsPath + avatarPath;

            var domainModel = Mapper.Map<DomainUser>(model);
            var result = userService.Register(domainModel, model.Password, AuthenticationManager);
            if (result != null)
            {
                string code = await userService.GenerateEmailConfirmationTokenAsync(result.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "User", new { userId = result.Id, code = code }, protocol: Request.Url.Scheme);
                await userService.SendEmailAsync(result.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return View("CheckEmail");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ViewProfile(int id)
        {
            var user = userService.GetUser(id);
            var userModel = Mapper.Map<ViewProfileViewModel>(user);
            return View(userModel);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await userService.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/EditProfile
        public ActionResult EditProfile(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.EditProfileSuccess ? "You profile data has been updated"
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = userService.GetUser(Int32.Parse(User.Identity.GetUserId()));
            var model = Mapper.Map<EditUserViewModel>(user);

            return View(model);
        }

        //
        // POST: /Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = userService.GetUser(Int32.Parse(User.Identity.GetUserId()));
            user = Mapper.Map<DomainUser>(model);
            var result = await userService.UpdateUserAsync(Int32.Parse(User.Identity.GetUserId()), user);
            if (result.Succeeded)
            {
                return RedirectToAction("ViewProfile", new { id = model.Id});
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            userService.SignOut(AuthenticationManager);

            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        public enum ManageMessageId
        {
            EditProfileSuccess,
            ChangePasswordSuccess,
            Error
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion

        public ActionResult UsersList()
        {
            var users = userService.GetAll().Select(Mapper.Map < DomainUser, UserViewModel>).AsEnumerable();
            return View(users);
        }
    }
}
