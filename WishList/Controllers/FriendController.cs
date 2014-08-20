using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace WishList.Controllers
{
    public class FriendController : Controller
    {
        //
        // GET: /Friend/
        private readonly IFriendService friendService;

        public FriendController(IFriendService iFriendService)
        {
            friendService = iFriendService;
        }

        public ActionResult AddFriend(int friendId)
        {
            friendService.Create(Int32.Parse(User.Identity.GetUserId()), friendId);
            return PartialView("_Success");
        }
        public ActionResult DeleteFriend(int friendId)
        {
            friendService.Create(Int32.Parse(User.Identity.GetUserId()), friendId);
            return PartialView("_Success");
        }

        public ActionResult FriendList()
        {
            var friends = friendService.GetAll().Where(f => f.UserId == Int32.Parse(User.Identity.GetUserId()));
            return PartialView("_FriendList", friends);
        }

    }
}
