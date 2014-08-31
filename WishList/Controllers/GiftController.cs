using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using WishList.ViewModels;
using WebGrease.Css.Extensions;

namespace WishList.Controllers
{
    public class GiftController : BaseController
    {
        private const int GiftsPerPage = 10;
        private readonly IGiftService _giftService;

        public GiftController(IUserService iUserService, IGiftService iGiftService)
            : base(iUserService)
        {
            _giftService = iGiftService;
        }

        public ActionResult Catalog(int pageNum = 0)
        {
            var gifts = _giftService.GetAll().ToList();
            var giftsCount = gifts.Count();
            gifts = gifts.Skip(GiftsPerPage*pageNum).Take(GiftsPerPage).ToList();
            int giftsPageNum = giftsCount % GiftsPerPage != 0 ? (giftsCount / GiftsPerPage + 1) : giftsCount / GiftsPerPage;
            var model = Mapper.Map<IEnumerable<GiftViewModel>>(gifts);

            model.ForEach(x => x.About =
               x.About.Length < CustomConstants.AboutGiftsLettersCount ?
               x.About :
               x.About.Substring(0, CustomConstants.AboutGiftsLettersCount) + CustomConstants.Dots);

            model.First().NumberOfPages = giftsPageNum;
            model.First().CurrentPage = pageNum;
            return View(model);
        }

        [RoleAuthorize(Roles = "Moderator")]
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
                var file = Request.Files["file"];

                var logoPath = "";

                if (file != null && file.ContentLength > 0)
                {
                    logoPath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath(StringResources.GiftsLogoPath), logoPath);
                    file.SaveAs(path);
                }
                else
                {
                    logoPath = StringResources.NoGiftLogoPath;
                }

                createGiftViewModel.Logo = StringResources.GiftsLogoPath + logoPath;

                var newGift = Mapper.Map<DomainGift>(createGiftViewModel);
                _giftService.Create(newGift);
                return RedirectToAction("CreateGiftSuccess");
            }

            ModelState.AddModelError("", "Invalid model");
            return View();
        }

        public ActionResult CreateGiftSuccess()
        {
            return View("CreateGiftSuccess");
        }

        [RoleAuthorize(Roles = "Moderator")]
        public ActionResult UpdateGift(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var gift = _giftService.Get(id);

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
                _giftService.Update(newGift);
                return PartialView("_UpdateGiftSuccessPartial", giftViewModel.Name);
            }
            else
            {
                ModelState.AddModelError("", "Invalid model");
            }
            return PartialView("_UdateGiftPartialView");
        }

        [RoleAuthorize(Roles = "Moderator")]
        public ActionResult DeleteGift(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var giftName = _giftService.Get(id).Name;
                _giftService.Delete(id);
                return PartialView("_DeleteGiftSuccessPartial", giftName);
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult ViewGift(int id)
        {
            var gift = _giftService.Get(id);
            if (gift == null)
            {
                throw new HttpException(404, "Category not found");
            }
            var model = Mapper.Map<GiftViewModel>(gift);
            if (CurrentUser != null)
            {
                model.ViewsCount = _giftService.ChangeViewsCount(gift.Id, CurrentUser.Id);
                model.DoesUserHaveWishlists = _giftService.HaveWishlists(CurrentUser.Id);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public int ChangeLikesCount(string id)
        {
            return _giftService.ChangeLikesCount(id, CurrentUser.Id);
        }

        [HttpPost]
        public bool EnableLikes()
        {
            return User.Identity.IsAuthenticated;
        }
        [HttpPost]
        public ActionResult GiftsSearch(string namePart)
        {
            return PartialView("_SearchResultPartial", _giftService.SearchGiftsByName(namePart).Select(Mapper.Map<DomainGift, GiftViewModel>).Take(5).AsEnumerable());
        }

        public ActionResult SearchResults(string id)
        {
            return View(_giftService.SearchGiftsByName(id).Select(Mapper.Map<DomainGift, GiftViewModel>).AsEnumerable());
        }
    }
}
