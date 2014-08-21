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
            Mapper.CreateMap<DomainUser, EditUserViewModel>();
            Mapper.CreateMap<DomainWishList, CreateWishListViewModel>();
            Mapper.CreateMap<DomainWishList, WishListViewModel>();//.AfterMap((domain, model) =>
           /* {
                model.Gifts = domain.Gifts;
            });*/
            Mapper.CreateMap<DomainGift, GiftViewModel>();
            #endregion

            #region WebUI to BLL

            Mapper.CreateMap<CreateUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateGiftViewModel, DomainGift>();
            Mapper.CreateMap<EditUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateGiftViewModel, DomainGift>();
            Mapper.CreateMap<CreateWishListViewModel, DomainWishList>();
            Mapper.CreateMap<GiftViewModel, DomainGift>();

            #endregion


        }
    }
}