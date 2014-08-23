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
    public class GiftController : Controller
    {
        private readonly IGiftService giftService;

        public GiftController(IGiftService iGiftService)
        {
            giftService = iGiftService;
        }

        public ActionResult Catalog()
        {
            var gifts = giftService.GetAll().Select(Mapper.Map < DomainGift, GiftViewModel>).AsQueryable();
            return View(gifts);
        }

        public ActionResult CreateGift()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateGiftPartial");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateGift(CreateGiftViewModel createGiftViewModel)
        {
            if (ModelState.IsValid)
            {
                var newGift = Mapper.Map<DomainGift>(createGiftViewModel);
                giftService.Create(newGift);
                return PartialView("_CreateGiftSuccessPartial", createGiftViewModel.Name);
            }

            ModelState.AddModelError("", "Invalid model");
            return View();
        }

        public ActionResult UpdateGift(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var gift = giftService.Get(id);

                return PartialView("_UdateGiftPartialView", Mapper.Map<GiftViewModel>(gift));
            }
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateGift(GiftViewModel giftViewModel)
        {
            if (ModelState.IsValid)
            {
                var newGift = Mapper.Map<DomainGift>(giftViewModel);
                giftService.Update(newGift);
                return PartialView("_UpdateGiftSuccessPartial", giftViewModel.Name);
            }
            else
            {
                ModelState.AddModelError("", "Invalid model");
            }
            return PartialView("_UdateGiftPartialView");
        }

        public ActionResult DeleteGift(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var giftName = giftService.Get(id).Name;
                giftService.Delete(id);
                return PartialView("_DeleteGiftSuccessPartial", giftName);
            }
            return View();
        }

        public ActionResult ViewGift(GiftViewModel model)
        {
            var gift = giftService.Get(model.Id);
            return View(Mapper.Map<GiftViewModel>(gift));

        }

        [HttpPost]
        public int ChangeLikesCount(string id)
        {
            return giftService.ChangeLikesCount(id);
        }
    }
}
