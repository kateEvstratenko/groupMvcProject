using AutoMapper;
using BLL.Models;
using DAL.Models;

namespace BLL.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void RegisterMappings()
        {
            #region DAL to BLL
            Mapper.CreateMap<Gift, DomainGift>();
            Mapper.CreateMap<WishList, DomainWishList>().ForMember(c => c.User, opt => opt.Ignore());
            Mapper.CreateMap<Comment,DomainComment>();
            Mapper.CreateMap<Friend, DomainFriend>();
            Mapper.CreateMap<Tag, DomainTag>();
            Mapper.CreateMap<User, DomainUser>();
            Mapper.CreateMap<View, DomainView>();
            Mapper.CreateMap<Vote, DomainVote>();
            #endregion

            #region BLL to DAL
            Mapper.CreateMap<DomainGift, Gift>();
            Mapper.CreateMap<DomainVote, Vote>();
            Mapper.CreateMap<DomainWishList, WishList>().ForMember(c => c.User, opt => opt.Ignore());
            Mapper.CreateMap<DomainView, View>();
            Mapper.CreateMap<DomainUser, User>();
            Mapper.CreateMap<DomainTag, Tag>();
            Mapper.CreateMap<DomainFriend, Friend>();
            Mapper.CreateMap<DomainComment, Comment>();
            #endregion
        }
    }
}
