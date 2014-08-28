using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Models;
using Microsoft.AspNet.Identity;
using WishList.ViewModels;

namespace WishList.Controllers
{
    public class FriendController : BaseController
    {
        //
        // GET: /Friend/
        private readonly IFriendService friendService;

        public FriendController(IUserService iUserService,IFriendService iFriendService) : base(iUserService)
        {
            friendService = iFriendService;
        }

        [Authorize]
        public ActionResult AddFriend(int friendId)
        {
            var check = friendService.Create(CurrentUser.Id, friendId);
            if (check)
            {
                return PartialView("_AddedFriendSuccessPartial", friendId);
            }
            return PartialView("_FriendAlredyInYourListPartial", friendId);
        }
        public ActionResult DeleteFriend(int id)
        {
            friendService.Delete(CurrentUser.Id, id);
            return PartialView("_DeleteFriendSuccessPartial", id);
        }
        [Authorize]
        public ActionResult FriendList()
        {
            var friends = friendService.GetAll(CurrentUser.Id).Select(Mapper.Map<DomainUser, UserViewModel>);
            return PartialView("_FriendListPartial", friends.AsEnumerable());
        }

        /*public IEnumerable<DomainFriend> GetAll()
        {
            var friends = friendService.GetAll(CurrentUser.Id).Select(Mapper.Map<DomainUser, UserViewModel>);
            var model = Mapper.Map<IEnumerable<DomainFriend>>(friends);
            return model;
        }*/
    }
}
