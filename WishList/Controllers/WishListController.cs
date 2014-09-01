using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;
using WishList.ViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class WishListController : BaseController
    {
        private readonly IWishListService _wishListService;
        private readonly IFriendService _friendService;

        public WishListController(IUserService iUserService, IWishListService iWishListService, IFriendService iFriendService)
            : base(iUserService)
        {
            _wishListService = iWishListService;
            _friendService = iFriendService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var friends = _friendService.GetAll(CurrentUser.Id).ToList();
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
                var friends = _friendService.GetAll(CurrentUser.Id).ToList();
                model.FriendsList = new MultiSelectList(friends, "Id", "UserName", model.FriendsId);
                return PartialView("_Create", model);
            }

            var domainWishList = Mapper.Map<DomainWishList>(model);

            if (model.FriendsId != null)
            {
                var friendsList =
                    _friendService.GetAllFriends(model.UserId)
                        .Where(x => model.FriendsId.Contains(x.FriendId.ToString()))
                        .ToList();
                domainWishList.Friends = friendsList;
            }

            var id = _wishListService.Create(domainWishList);

            return Json(new { success = true, newWishListId = id });
        }

        public ActionResult Delete(int id)
        {
            _wishListService.Delete(id);
            return new EmptyResult();
        }

        public ActionResult Update(WishListViewModel model)
        {
            var domainWishList = Mapper.Map<DomainWishList>(model);
            _wishListService.Update(domainWishList);
            return RedirectToAction("GetAllWishListsOfUser");//ManageProfile
        }

        public ActionResult ViewWishList(int id)
        {
            var wishList = _wishListService.Get(id);
            var wishListViewModel = Mapper.Map<WishListViewModel>(wishList);
            return View(wishListViewModel);
        }

        public ActionResult ViewWishListPartial(int id)
        {
            var wishList = _wishListService.Get(id);
            var wishListViewModel = Mapper.Map<WishListViewModel>(wishList);
            return View("_ViewWishListPartial", wishListViewModel);
        }

        public ActionResult GenerateLink(int id, string url)
        {
            _wishListService.GenerateLink(id, url);
            return RedirectToAction("ViewWishList", new { id = id });
        }

        [AllowAnonymous]
        public ActionResult GetAllWishListsOfUser(int userId)
        {
            var allWishLists = _wishListService.GetAllWishListsOfUser(userId);
            if (userId != CurrentUser.Id)
            {
                allWishLists = allWishLists.Where(w => w.Friends.Count(f => f.FriendId == CurrentUser.Id) != 0);
            }

            var model = Mapper.Map<IEnumerable<WishListViewModel>>(allWishLists.ToList());
            return PartialView("_UsersWishLists", model);
        }

        public ActionResult GetAllUsersWishListsOfGift(int giftId)
        {
            var userId = CurrentUser.Id;
            var wishListsOfGift = _wishListService.GetAllUsersWishListsOfGift(giftId, userId).ToList();

            if (wishListsOfGift.Count < 1)
                return new EmptyResult();

            var model = Mapper.Map<IEnumerable<UsersWishListsOfGiftViewModel>>(wishListsOfGift);
            model.ForEach(x => x.GiftId = giftId);

            return PartialView("_GetAllUsersWishListsOfGift", model);
        }

        public ActionResult AddGiftToWishList(WishListDropDownViewModel model)
        {
            _wishListService.AddGiftToWishList(model.GiftId, Int32.Parse(model.WishListId));
            return Json(new { success = true });
        }

        public ActionResult DeleteGiftFromWishList(int giftId, int wishListId, string actionName)
        {
            _wishListService.DeleteGiftFromWishList(giftId, wishListId);

            if (actionName == "GetAllUsersWishListsOfGift")
            {
                return RedirectToAction("GetAllUsersWishListsOfGift", new { giftId = giftId });
            }

            return new EmptyResult();
        }

        public ActionResult GetDropDownWishLists(int giftId)
        {
            var userId = CurrentUser.Id;

            var wishLists = _wishListService.GetUsersWishListsWithoutGift(giftId, userId).ToList();

            var model = new WishListDropDownViewModel()
            {
                GiftId = giftId,
                DropDownList = new SelectList(wishLists, "Id", "Name"),
            };
            return PartialView("_GetDropDownWishLists", model);
        }

        public ActionResult GetVotesCount(string wishListId, string giftId)
        {
            return PartialView("_VotesCountPartial", _wishListService.GetVotesCount(wishListId, giftId));
        }

        [HttpPost]
        [Authorize]
        public string ChangeVotesCount(string id)
        {
            var json = JsonConvert.SerializeObject(
                _wishListService.ChangeVotesCount(id, CurrentUser.Id).ToArray());
            return json;
        }

        [HttpPost]
        public bool EnableVotes(string id)
        {
            return User.Identity.IsAuthenticated && _wishListService.CheckCurrentUserInWishList(CurrentUser.Id, Int32.Parse(id));
        }
    }
}
