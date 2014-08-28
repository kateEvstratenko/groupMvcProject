using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;
using WishList.ViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class WishListController : BaseController
    {
        private readonly IWishListService wishListService;
        private readonly IFriendService friendService;

        public WishListController(IUserService iUserService, IWishListService iWishListService, IFriendService iFriendService) : base(iUserService)
        {
            wishListService = iWishListService;
            friendService = iFriendService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var friends = friendService.GetAll(CurrentUser.Id).ToList();
            var model = new CreateWishListViewModel()
            {
                UserId = CurrentUser.Id,
                FriendsList = new MultiSelectList(friends, "Id", "UserName")
            };
            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult Create(CreateWishListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var friends = friendService.GetAll(CurrentUser.Id).ToList();
                model.FriendsList = new MultiSelectList(friends, "Id", "UserName", model.FriendsId);
                return PartialView("_Create", model);
            }

            var domainWishList = Mapper.Map<DomainWishList>(model);

            if (model.FriendsId != null)
            {
                var friendsList =
                    friendService.GetAllFriends(model.UserId)
                        .Where(x => model.FriendsId.Contains(x.Id.ToString()))
                        .ToList();
                domainWishList.Friends = friendsList;
            }

            var id = wishListService.Create(domainWishList);

            return Json(new { success = true, newWishListId = id });
        }

        public ActionResult Delete(int id)
        {
            wishListService.Delete(id);
            return new EmptyResult();
        }

        public ActionResult Update(WishListViewModel model)
        {
            var domainWishList = Mapper.Map<DomainWishList>(model);
            wishListService.Update(domainWishList);
            return RedirectToAction("GetAllWishListsOfUser");//ManageProfile
        }

        public ActionResult ViewWishList(int id)
        {
            var wishList = wishListService.Get(id);
            var wishListViewModel = Mapper.Map<WishListViewModel>(wishList);
            return View(wishListViewModel);
        }

        public ActionResult ViewWishListPartial(int id)
        {
            var wishList = wishListService.Get(id);
            var wishListViewModel = Mapper.Map<WishListViewModel>(wishList);
            return View("_ViewWishListPartial", wishListViewModel);
        }

        public ActionResult GenerateLink(int id, string url)
        {
            wishListService.GenerateLink(id, url);
            return RedirectToAction("ViewWishList", new { id = id });
        }

        [AllowAnonymous]
        public ActionResult GetAllWishListsOfUser()
        {
            if (CurrentUser != null)
            {
                var userId = CurrentUser.Id;
                var wishLists = wishListService.GetAllWishListsOfUser(userId).ToList();
                var model = Mapper.Map<IEnumerable<WishListViewModel>>(wishLists);
                return PartialView("_UsersWishLists", model);
            }
            return new EmptyResult();
        }

        public ActionResult GetAllUsersWishListsOfGift(int giftId)
        {
            var userId = CurrentUser.Id;
            var wishListsOfGift = wishListService.GetAllUsersWishListsOfGift(giftId, userId).ToList();

            if (wishListsOfGift.Count < 1)
                return new EmptyResult();

            var model = Mapper.Map<IEnumerable<UsersWishListsOfGiftViewModel>>(wishListsOfGift);
            model.ForEach(x => x.GiftId = giftId);

            return PartialView("_GetAllUsersWishListsOfGift", model);
        }

        public ActionResult AddGiftToWishList(WishListDropDownViewModel model)
        {
            wishListService.AddGiftToWishList(model.GiftId, Int32.Parse(model.WishListId));
            //return GetAllUsersWishListsOfGift(model.GiftId);
            return Json(new { success = true });
        }

        public ActionResult DeleteGiftFromWishList(int giftId, int wishListId, string actionName)
        {
            wishListService.DeleteGiftFromWishList(giftId, wishListId);

            if (actionName == "GetAllUsersWishListsOfGift")
            {
                return RedirectToAction("GetAllUsersWishListsOfGift", new { giftId = giftId });
            }

            return new EmptyResult();
            //return RedirectToAction("ViewWishList", new { id = wishListId });
        }

        public ActionResult GetDropDownWishLists(int giftId)
        {
            var userId = CurrentUser.Id;

            var wishLists = wishListService.GetUsersWishListsWithoutGift(giftId, userId).ToList();

            var model = new WishListDropDownViewModel()
            {
                GiftId = giftId,
                DropDownList = new SelectList(wishLists, "Id", "Name"),
            };
            return PartialView("_GetDropDownWishLists", model);
        }

        public ActionResult GetVotesCount(string wishListId, string giftId)
        {
            return PartialView("_VotesCountPartial", wishListService.GetVotesCount(wishListId, giftId));
        }

        [HttpPost]
        [Authorize]
        public string ChangeVotesCount(string id)
        {
            var json =
                JsonConvert.SerializeObject(
                    wishListService.ChangeVotesCount(id, Int32.Parse(User.Identity.GetUserId())).ToArray());
            return json;
        }

        [HttpPost]
        public bool EnableVotes()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
