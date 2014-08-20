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
<<<<<<< HEAD
            #endregion

            #region WebUI to BLL

            Mapper.CreateMap<CreateUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateGiftViewModel, DomainGift>();
=======
            Mapper.CreateMap<DomainUser, EditUserViewModel>();
            #endregion

            #region WebUI to BLL
            Mapper.CreateMap<EditUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateUserViewModel, DomainUser>();
            Mapper.CreateMap<CreateGiftViewModel,DomainGift>();
>>>>>>> 6770845f2786c1d94c10b590cb1f18ddfc41d53f
            #endregion
        }
    }
}