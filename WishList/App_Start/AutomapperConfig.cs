using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using AutoMapper;
using BLL.Models;
using DAL.Models;
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
            Mapper.CreateMap<DomainWishList, WishListViewModel>();
            Mapper.CreateMap<DomainWishList, WishListDropDownViewModel>();
            //.AfterMap((domain, model) =>
           /* {
                model.Gifts = domain.Gifts;
            });*/
            Mapper.CreateMap<DomainUser, UserViewModel>();
            Mapper.CreateMap<DomainUser, EditUserViewModel>().AfterMap((user, viewmodel) =>
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                viewmodel.FormattedBirthday = user.Birthday.ToShortDateString();
                Thread.CurrentThread.CurrentCulture = originalCulture;
            });
            Mapper.CreateMap<DomainUser, ViewProfileViewModel>().AfterMap((user, viewmodel) =>
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                viewmodel.FormattedBirthday = user.Birthday.ToShortDateString();
                Thread.CurrentThread.CurrentCulture = originalCulture;
            });
            Mapper.CreateMap<DomainGift, GiftViewModel>();
            Mapper.CreateMap<DomainUser, UserViewModel>().AfterMap((user, viewmodel) =>
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                viewmodel.FormattedBirthday = user.Birthday.ToShortDateString();
                Thread.CurrentThread.CurrentCulture = originalCulture;
            });
            #endregion

            #region WebUI to BLL
            Mapper.CreateMap<EditUserViewModel, DomainUser>().AfterMap((viewmodel, user) =>
            {
                IFormatProvider culture = new CultureInfo("fr-FR", true);
                user.Birthday = new DateTime();
                user.Birthday = DateTime.Parse(viewmodel.FormattedBirthday, culture, DateTimeStyles.AssumeLocal);
            });
            Mapper.CreateMap<CreateUserViewModel, DomainUser>().AfterMap((viewmodel, user) =>
            {
                IFormatProvider culture = new CultureInfo("fr-FR", true);
                user.Birthday = new DateTime();
                user.Birthday = DateTime.Parse(viewmodel.FormattedBirthday, culture, DateTimeStyles.AssumeLocal);
            });
            Mapper.CreateMap<CreateGiftViewModel, DomainGift>();
            Mapper.CreateMap<CreateUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateGiftViewModel, DomainGift>();
            Mapper.CreateMap<CreateWishListViewModel, DomainWishList>();
            Mapper.CreateMap<DomainWishList, WishListViewModel>();
            Mapper.CreateMap<GiftViewModel, DomainGift>();
            Mapper.CreateMap<UserViewModel, DomainUser>();

            #endregion


        }
    }
}