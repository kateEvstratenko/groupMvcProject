using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using WishList.ViewModels;

namespace WishList.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly IGiftService giftService;
        public HomeController(IGiftService iGiftService)
        {
            giftService = iGiftService;
        }

        public ActionResult Index()
        {
            var gifts = giftService.GetPolular(10).Select(Mapper.Map<DomainGift, GiftViewModel>);
            return View(gifts);
        }

    }
}
