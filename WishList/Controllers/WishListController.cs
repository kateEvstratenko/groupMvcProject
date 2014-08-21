using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using WishList.ViewModels;

namespace WishList.Controllers
{
    public class WishListController : Controller
    {
        private readonly IWishListService wishListService;

        public WishListController(IWishListService iWishListService)
        {
            wishListService = iWishListService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            /*var domainWishList = Mapper.Map<DomainWishList>(model);
            wishListService.Create(domainWishList);
            return RedirectToAction("GetAllWishListsOfUser");//ManageProfile*/
            var model = new CreateWishListViewModel()
            {
                UserId = Int32.Parse(User.Identity.GetUserId())
            };
            return PartialView("_Create",  model);
        }

        [HttpPost]
        public ActionResult Create(CreateWishListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", model);
            }

            var domainWishList = Mapper.Map<DomainWishList>(model);
            wishListService.Create(domainWishList);
            return RedirectToAction("GetAllWishListsOfUser");
        }

        public ActionResult Delete(int id)
        {
            wishListService.Delete(id);
            return RedirectToAction("GetAllWishListsOfUser");//ManageProfile
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

        public ActionResult GetAllWishListsOfUser()
        {
            var userId = Int32.Parse(User.Identity.GetUserId());
            var wishLists = wishListService.GetAllWishListsOfUser(userId).ToList();
            var model = Mapper.Map<IEnumerable<WishListViewModel>>(wishLists);
            return View("_UsersWishLists", model);
        }
    }
}
