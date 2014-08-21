using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AutoMapper;
using BLL.Models;
using WishList.ViewModels;

namespace WishList
{
    public class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            #region BLL to WebUi
            Mapper.CreateMap<DomainGift, CreateGiftViewModel>();
            Mapper.CreateMap<DomainGift, GiftViewModel>();
            Mapper.CreateMap<DomainWishList, CreateWishListViewModel>();
            Mapper.CreateMap<DomainWishList, WishListViewModel>();//.AfterMap((domain, model) =>
           /* {
                model.Gifts = domain.Gifts;
            });*/
            Mapper.CreateMap<DomainUser, EditUserViewModel>().AfterMap((user, viewmodel) =>
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                viewmodel.Birthday1 = user.Birthday.ToShortDateString();
                Thread.CurrentThread.CurrentCulture = originalCulture;
            });
            Mapper.CreateMap<DomainUser, ViewProfileViewModel>().AfterMap((user, viewmodel) =>
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                viewmodel.Birthday = user.Birthday.ToShortDateString();
                Thread.CurrentThread.CurrentCulture = originalCulture;
            });
            Mapper.CreateMap<DomainGift, GiftViewModel>();
            #endregion

            #region WebUI to BLL
            Mapper.CreateMap<EditUserViewModel, DomainUser>().AfterMap((viewmodel, user) =>
            {
                IFormatProvider culture = new CultureInfo("fr-FR", true);
                user.Birthday = new DateTime();
                user.Birthday = DateTime.Parse(viewmodel.Birthday1, culture, DateTimeStyles.AssumeLocal);
            });
            Mapper.CreateMap<CreateUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateGiftViewModel, DomainGift>();
            Mapper.CreateMap<CreateUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateGiftViewModel, DomainGift>();
            Mapper.CreateMap<CreateWishListViewModel, DomainWishList>();
            Mapper.CreateMap<GiftViewModel, DomainGift>();

            #endregion


        }
    }
}