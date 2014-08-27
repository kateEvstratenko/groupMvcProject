using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using WishList.ViewModels;

namespace WishList.Controllers
{
    [RoleAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService iAdminService)
        {
            adminService = iAdminService;
        }

        public ActionResult ShowAllUsers()
        {
            var users = adminService.GetUsers();
            var viewmodels = users.Select(Mapper.Map<DomainUser, UserViewModel>).ToList();
            foreach (var item in viewmodels)
            {
                item.Roles = adminService.GetRoles(item.RoleId);
            }
            return View(viewmodels);
        }

        public ActionResult SwitchRole(int userId, int roleId)
        {
            adminService.SwitchRole(userId, roleId);
            return RedirectToAction("ShowAllUsers");
        }

        public ActionResult DeleteUser(int userId)
        {
            ViewBag.user = userId;
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteUserConfirm(int userId)
        {
            adminService.DeleteUser(userId);
            return PartialView("Success");
        }

        public ActionResult DeleteWishlist(int wishlistId)
        {
            adminService.DeleteWishlist(wishlistId);
            return RedirectToAction("Index", "Home");
        }

        public class RoleAuthorizeAttribute : AuthorizeAttribute
        {
            private string redirectUrl = "";
            public RoleAuthorizeAttribute()
                : base()
            {
            }

            public RoleAuthorizeAttribute(string redirectUrl)
                : base()
            {
                this.redirectUrl = redirectUrl;
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    string authUrl = redirectUrl; //passed from attribute

                    //if null, get it from config
                    if (String.IsNullOrEmpty(authUrl))
                        authUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["RolesAuthRedirectUrl"];

                    if (!String.IsNullOrEmpty(authUrl))
                        filterContext.HttpContext.Response.Redirect(authUrl);
                }

                //else do normal process
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
