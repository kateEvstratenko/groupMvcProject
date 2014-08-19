using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        ////
        //// POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = await userService.LoginAsync(model.UserName, model.Password, model.RememberMe, AuthenticationManager);
        //    if (user != null)
        //    {
        //        return RedirectToLocal(returnUrl);
        //    }
            
        //    ModelState.AddModelError("", "Invalid username or password.");
            
        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        ////
        //// POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var result = await userService.RegisterAsync(model.UserName, model.Password, AuthenticationManager);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    AddErrors(result);
            
        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// GET: /Account/EditProfile
        //public ActionResult EditProfile(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.EditProfileSuccess? "You profile data has been updated"
        //        : message == ManageMessageId.Error ? "An error has occurred."
        //        : "";

        //    var user = userService.GetUser(Int32.Parse(User.Identity.GetUserId()));
        //    var model = Mapper.Map<EditUserViewModel>(user);

        //    return View(model);
        //}

        ////
        //// POST: /Account/EditProfile
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EditProfile(EditUserViewModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    var user = userService.GetUser(Int32.Parse(User.Identity.GetUserId()));
        //    user = Mapper.Map<DomainUser>(model);
        //    var result = await userService.UpdateUserAsync(Int32.Parse(User.Identity.GetUserId()), user);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("EditProfile", new { Message = ManageMessageId.EditProfileSuccess });
        //    }
        //    AddErrors(result);
        //    return View(model);
        //}

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

    }
}
