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
            return View();
        }

        [HttpPost]
        public ActionResult CreateGift(CreateGiftViewModel createGiftViewModel)
        {
            if (ModelState.IsValid)
            {
                var newGift = Mapper.Map<DomainGift>(createGiftViewModel);
                giftService.Create(newGift);
            }
            else
            {
                ModelState.AddModelError("", "Invalid model");
            }
            return View(createGiftViewModel);
        }

        public ActionResult UpdateGift(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var gift = giftService.Get(id);

                return PartialView("_UdateGiftPartialView", Mapper.Map<GiftViewModel>(gift));
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateGift(GiftViewModel giftViewModel)
        {
            if (ModelState.IsValid)
            {
                var newGift = Mapper.Map<DomainGift>(giftViewModel);
                giftService.Update(newGift);
                return PartialView("_Success");
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
                giftService.Delete(id);
                return PartialView("_Success");
            }
            return View();
        }

        public ActionResult ViewGift(GiftViewModel model)
        {
            var gift = giftService.Get(model.Id);
            return View(Mapper.Map<GiftViewModel>(gift));

        }
    }
}
