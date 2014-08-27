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
            Mapper.CreateMap<DomainComment, CreateCommentViewModel>();
            Mapper.CreateMap<DomainComment, CommentViewModel>();
            Mapper.CreateMap<DomainGift, CreateGiftViewModel>();
            Mapper.CreateMap<DomainGift, GiftViewModel>();
            Mapper.CreateMap<DomainWishList, CreateWishListViewModel>();
            Mapper.CreateMap<DomainWishList, WishListViewModel>();
            Mapper.CreateMap<DomainWishList, WishListDropDownViewModel>();

            Mapper.CreateMap<DomainWishList, UsersWishListsOfGiftViewModel>();
             
            Mapper.CreateMap<DomainUser, UserViewModel>();
            Mapper.CreateMap<DomainUser, EditUserViewModel>().AfterMap((user, viewmodel) =>
            {
                viewmodel.FormattedBirthday = String.Format("{0:d/M/yyyy}", user.Birthday);
            });
            Mapper.CreateMap<DomainUser, ViewProfileViewModel>().AfterMap((user, viewmodel) =>
            {
                viewmodel.FormattedBirthday = String.Format("{0:d/M/yyyy}", user.Birthday);
            });
            Mapper.CreateMap<DomainGift, GiftViewModel>();
            Mapper.CreateMap<DomainUser, UserViewModel>().AfterMap((user, viewmodel) =>
            {
                viewmodel.FormattedBirthday = String.Format("{0:d/M/yyyy}", user.Birthday);
            });
            #endregion

            #region WebUI to BLL

            
            Mapper.CreateMap<EditUserViewModel, DomainUser>().AfterMap((viewmodel, user) =>
            {
                user.Birthday = DateTime.Parse(viewmodel.FormattedBirthday, new CultureInfo("fr-FR", true), DateTimeStyles.AssumeLocal);
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
            Mapper.CreateMap<CreateCommentViewModel, DomainComment>();
            Mapper.CreateMap<CreateWishListViewModel, DomainWishList>();
            Mapper.CreateMap<DomainWishList, WishListViewModel>();
            Mapper.CreateMap<GiftViewModel, DomainGift>();
            Mapper.CreateMap<UserViewModel, DomainUser>();
            Mapper.CreateMap<CommentViewModel, DomainComment>();
            #endregion

            Mapper.CreateMap<CommentViewModel, CreateCommentViewModel>();
            Mapper.CreateMap<CreateCommentViewModel, CommentViewModel>();
        }
    }
}