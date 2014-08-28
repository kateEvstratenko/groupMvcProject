using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;

namespace WishList.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IUserService UserService;

        protected BaseController(IUserService iUserService)
        {
            UserService = iUserService;
        }

        private DomainUser currentUser = null;
        private bool isUserInitialized = false;
        protected DomainUser CurrentUser
        {
            get
            {
                if (!isUserInitialized && Request.IsAuthenticated)
                {
                    currentUser = UserService.GetUser(User.Identity.GetUserId<int>());
                    isUserInitialized = true;
                }
                return currentUser;
            }
        }

        public class RoleAuthorizeAttribute : AuthorizeAttribute
        {
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    const string authUrl = "http://localhost:52994/Home/NotFound";

                    if (!String.IsNullOrEmpty(authUrl))
                        filterContext.HttpContext.Response.Redirect(authUrl);
                }

                //else do normal process
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
