﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using WishList.ViewModels;

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
            friendService.Delete(Int32.Parse(User.Identity.GetUserId()), friendId);
            return PartialView("_Success");
        }

        public ActionResult FriendList()
        {
            var friends = friendService.GetAll(Int32.Parse(User.Identity.GetUserId())).Select(Mapper.Map<DomainUser, UserViewModel>);
            return PartialView("_FriendListPartial", friends);
        }

    }
}
