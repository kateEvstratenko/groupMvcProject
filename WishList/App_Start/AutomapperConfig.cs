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

            Mapper.CreateMap<DomainComment, CommentViewModel>();
            Mapper.CreateMap<DomainGift, GiftViewModel>();
            Mapper.CreateMap<DomainUser,UserViewModel>();
            Mapper.CreateMap<DomainWishList, WishListViewModel>();

            #endregion

            #region WebUI to BLL

            #endregion
        }
    }
}