using System.Web.Mvc;
using BLL;
using BLL.Interfaces;
using BLL.Services;

namespace WishList.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly IGiftService _giftService;
        public HomeController(IGiftService giftService)
        {
            _giftService = giftService;
        }

        public ActionResult Index()
        {
            var gifts = _giftService.GetPolular(Constants.PopularGiftsCount);
            return View(gifts);
        }

    }
}
