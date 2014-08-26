using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL;
using BLL.Interfaces;
using WebGrease.Css.Extensions;
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
            var gifts = giftService.GetPolular(CustomConstants.PopularGiftsCount).ToList();
            var model = Mapper.Map<IEnumerable<GiftViewModel>>(gifts);
            model.ForEach(x => x.About = 
                x.About.Length < CustomConstants.AboutGiftsLettersCount ? 
                x.About : 
                x.About.Substring(0, CustomConstants.AboutGiftsLettersCount) + CustomConstants.Dots);

            return View(model);
        }

    }
}
