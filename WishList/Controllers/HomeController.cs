using System.Linq;
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
        private readonly IGiftService giftService;
        public HomeController(IGiftService iGiftService)
        {
            giftService = iGiftService;
        }

        public ActionResult Index()
        {
            var gifts = giftService.GetPolular(10);
            return View(gifts);
        }

    }
}
