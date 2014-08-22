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
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService iAdminService)
        {
            adminService = iAdminService;
        }

        public ActionResult Users()
        {
            var users = adminService.GetUsers();
            var viewmodels = users.Select(Mapper.Map<DomainUser,UserViewModel>).ToList();
            foreach (var item in viewmodels)
            {
                item.Roles = adminService.GetRoles(item.RoleId);
            }
            return View(viewmodels);
        }

    }
}
