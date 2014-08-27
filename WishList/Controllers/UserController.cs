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
    public class UserController : BaseController
    {

        public UserController(IUserService iUserService) : base(iUserService)
        {
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserService.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                if (user.EmailConfirmed == true)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", @"Email is not confirmed.");
                    return View(model);
                }
            }

            ModelState.AddModelError("", @"Invalid username or password.");

            return View(model);
        }

        private async Task SignInAsync(DomainUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await UserService.GenerateClaimAsync(user));
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

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
            var result = UserService.Register(domainModel, model.Password, AuthenticationManager);
            if (result != null)
            {
                string code = await UserService.GenerateEmailConfirmationTokenAsync(result.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "User", new { userId = result.Id, code = code }, protocol: Request.Url.Scheme);
                await UserService.SendEmailAsync(result.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return View("CheckEmail");
            }

            return View(model);
        }

        public ActionResult ViewProfile(int id)
        {
            var user = UserService.GetUser(id);
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

            IdentityResult result = await UserService.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            AddErrors(result);
            return View();
        }

        public ActionResult EditProfile(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.EditProfileSuccess ? "You profile data has been updated"
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = UserService.GetUser(CurrentUser.Id);
            var model = Mapper.Map<EditUserViewModel>(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserService.GetUser(CurrentUser.Id);
            user = Mapper.Map<DomainUser>(model);
            var result = await UserService.UpdateUserAsync(CurrentUser.Id, user);
            if (result.Succeeded)
            {
                return RedirectToAction("Success");
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            UserService.SignOut(AuthenticationManager);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult NoAccess()
        {
            return View();
        }

        public ActionResult RedirectToMain()
        {
            return RedirectToAction("Index","Home");
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
            var users = UserService.GetAll().Select(Mapper.Map < DomainUser, UserViewModel>).AsEnumerable();
            return View(users);
        }
    }
}
