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
        IQueryable<DomainWishList> GetAllWishListsOfUser(int userId);
        void GenerateLink(int id, string url);
        void AddGiftToWishList(int giftId, int wishListId);
    }
}
