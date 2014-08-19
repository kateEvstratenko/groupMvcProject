using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace WishList.Controllers
{
    public class WishListController : Controller
    {
        private readonly IWishListService wishListService;

        public WishListController(IWishListService iWishListService)
        {
            wishListService = iWishListService;
        }

        /*public ActionResult Create(WishListViewModel model)
        {
            var domainWishList = Mapper.Map<DomainWishList>(model);
            wishListService.Create(domainWishList);
            return RedirectToAction("GetAll");
        }*/

        /*public ActionResult Delete(int id)
        {
            wishListService.Delete(id);
            return RedirectToAction("GetAll");
        }*/

        /*public ActionResult Update(WishListViewModel model)
        {
            var domainWishList = Mapper.Map<DomainWishList>(model);
            wishListService.Update(domainWishList);
            return RedirectToAction("GetAll");
        }*/

        /*public ActionResult Get(int id)
        {
            var wishList = wishListService.Get(id);
            var wishListViewModel = Mapper.Map<WishListViewModel>(wishList);
            return View(wishListViewModel);
        }*/

        /*public ActionResult GetAll()
        {
            var wishLists = wishListService.GetAll();
            var model = Mapper.Map<IEnumerable<WishListViewModel>>(wishLists);
            return View(model);
        }*/

        /*public ActionResult GetAllWishListsOfUser()
        {
            var userId = Int32.Parse(User.Identity.GetUserId());
            var wishLists = wishListService.GetAllWishListsOfUser(userId);
            var model = Mapper.Map<IEnumerable<WishListViewModel>>(wishLists);
            return View(model);
        }*/
    }
}
