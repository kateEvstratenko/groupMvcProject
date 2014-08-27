using System.Collections.Generic;
using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IWishListService
    {
        void Create(DomainWishList domainWishList);
        void Delete(int id);
        void Update(DomainWishList domainWishList);
        DomainWishList Get(int id);
        void GenerateLink(int id, string url);
        void AddGiftToWishList(int giftId, int wishListId);
        void DeleteGiftFromWishList(int giftId, int wishListId);
        IQueryable<DomainWishList> GetAllWishListsOfUser(int userId);
        IQueryable<DomainWishList> GetAllUsersWishListsOfGift(int giftId, int userId);
        IQueryable<DomainWishList> GetUsersWishListsWithoutGift(int giftId, int userId);
        List<int> ChangeVotesCount(string id, int userId);
        int GetVotesCount(string wishListId, string giftId);
    }
}
