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
                string authUrl = "http://localhost:52994/Home/NotFound"; //passed from attribute

                if (!String.IsNullOrEmpty(authUrl))
                    filterContext.HttpContext.Response.Redirect(authUrl);
            }

            //else do normal process
            base.HandleUnauthorizedRequest(filterContext);
        }
    }

    [RoleAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        const int UsersPerPage = 3;

        private readonly IAdminService adminService;

        public AdminController(IAdminService iAdminService)
        {
            adminService = iAdminService;
        }

        public ActionResult ShowAllUsers(int pageNum = 0)
        {
            var users = adminService.GetUsers();
            var viewmodels = users.Select(Mapper.Map<DomainUser, UserViewModel>).Skip(UsersPerPage*pageNum).Take(UsersPerPage).ToList();
            int usersPageNum = users.ToList().Count % UsersPerPage != 0 ? (users.ToList().Count / UsersPerPage + 1) : users.ToList().Count/UsersPerPage;
            foreach (var item in viewmodels)
            {
                item.Roles = adminService.GetRoles(item.RoleId);
            }
            viewmodels[0].NumberOfPages = usersPageNum;
            viewmodels[0].CurrentPage = pageNum;
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
    }
}
