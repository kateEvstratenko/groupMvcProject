using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
//using DAL.Models;
using System.Web.Mvc;
using DAL.Models;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class WishListService: BaseService, IWishListService
    {
        public WishListService(IUnitOfWork uow) : base(uow) { }

        public void Create(DomainWishList domainWishList)
        {
            var wishList = Mapper.Map<WishList>(domainWishList);
            Uow.WishListRepository.Insert(wishList);
            Uow.Commit();
        }

        public void Delete(int id)
        {
            Uow.WishListRepository.Delete(id);
            Uow.Commit();
        }

        public void Update(DomainWishList domainWishList)
        {
            var currentWishList = Uow.WishListRepository.Get(domainWishList.Id);
            Mapper.Map<DomainWishList, WishList>(domainWishList, currentWishList);
            //currentWishList.Link = domainWishList.Link;
            Uow.WishListRepository.Update(currentWishList);
            Uow.Commit();
        }

        public DomainWishList Get(int id)
        {
            var wishList = Uow.WishListRepository.Get(id);
            var domainWishList = Mapper.Map<DomainWishList>(wishList);
            return domainWishList;
        }

        /*public IQueryable<DomainWishList> GetAll()
        {
            var wishLists = Uow.WishListRepository.GetAll();
            var domainWishLists = wishLists.Select(Mapper.Map<WishList, DomainWishList>);
            return domainWishLists.AsQueryable();
        }*/

        public IQueryable<DomainWishList> GetAllWishListsOfUser(int userId)
        {
            var wishLists = Uow.WishListRepository.GetAll().Where(x => x.UserId == userId);
            var domainWishLists = wishLists.Select(Mapper.Map<WishList, DomainWishList>);
            return domainWishLists.AsQueryable();
        }

        public void GenerateLink(int id, string url)
        {
            var wishList = Get(id);
            wishList.Link = url;
            Update(wishList);
        }

        public void AddGiftToWishList(int giftId, int wishListId)
        {
            var gift = Uow.GiftRepository.Get(giftId);
            var wishList = Uow.WishListRepository.Get(wishListId);
            wishList.Gifts.Add(gift);

            //Mapper.Map<DomainWishList, WishList>(domainWishList, currentWishList);

            Uow.WishListRepository.Update(wishList);
            Uow.Commit();
        }
    }
}
