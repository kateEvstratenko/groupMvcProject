using System.Web.Mvc;
using BLL;
using BLL.Services;

namespace WishList.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly GiftService _giftService;
        public HomeController(GiftService giftService)
        {
            _giftService = giftService;
        }

        public ActionResult Index()
        {
            var gifts = _giftService.GetPopularGifts(Constants.PopularGiftsCount);
            return View(gifts);
        }

    }
}
