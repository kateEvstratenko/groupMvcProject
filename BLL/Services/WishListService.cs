using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using System.Web.Mvc;
using DAL.Models;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class WishListService: BaseService, IWishListService
    {
        public WishListService(IUnitOfWork uow) : base(uow) { }

        public int Create(DomainWishList domainWishList)
        {
            var wishList = Mapper.Map<WishList>(domainWishList);

            wishList.Friends = wishList.Friends.Select(x => Uow.FriendRepository.Get(x.Id)).ToList();
            Uow.WishListRepository.Insert(wishList);
            Uow.Commit();

            return wishList.Id;
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


            Uow.WishListRepository.Update(currentWishList);
            Uow.Commit();
        }

        public DomainWishList Get(int id)
        {
            var wishList = Uow.WishListRepository.Get(id);
            var domainWishList = Mapper.Map<DomainWishList>(wishList);
            return domainWishList;
        }

        public void GenerateLink(int id, string url)
        {
            var wishList = Get(id);
            wishList.Link = url;
            Update(wishList);
        }

        public IQueryable<DomainWishList> GetAllWishListsOfUser(int userId)
        {
            var wishLists = Uow.WishListRepository.GetAll().Where(x => x.UserId == userId);
            var domainWishLists = wishLists.Select(Mapper.Map<WishList, DomainWishList>);
            return domainWishLists.AsQueryable();
        }

        public IQueryable<DomainWishList> GetAllUsersWishListsOfGift(int giftId, int userId)
        {
            var wishLists = GetAllWishListsOfUser(userId).Where(x => x.Gifts.Any(g => g.Id == giftId));
            return wishLists;
        }

        public void AddGiftToWishList(int giftId, int wishListId)
        {
            var gift = Uow.GiftRepository.Get(giftId);
            var wishList = Uow.WishListRepository.Get(wishListId);
            wishList.Gifts.Add(gift);

            Uow.WishListRepository.Update(wishList);
            Uow.Commit();
        }

        public void DeleteGiftFromWishList(int giftId, int wishListId)
        {
            var gift = Uow.GiftRepository.Get(giftId);
            var wishList = Uow.WishListRepository.Get(wishListId);
            wishList.Gifts.Remove(gift);

            Uow.WishListRepository.Update(wishList);
            Uow.Commit();
        }

        public IQueryable<DomainWishList> GetUsersWishListsWithoutGift(int giftId, int userId)
        {
            var usersWishListsOfGift = GetAllUsersWishListsOfGift(giftId, userId).ToList();
            var allUsersWishLists = GetAllWishListsOfUser(userId).ToList();

            foreach (var item in usersWishListsOfGift)
            {
                allUsersWishLists.RemoveAll(x => x.Id == item.Id);
            }

            return allUsersWishLists.AsQueryable();
        }
        public List<int> ChangeVotesCount(string id, int userId)
        {
            var getId = new Regex("[0-9]+");
            var m = getId.Matches(id);
            var giftId = Int32.Parse(m[0].Value);
            var wishListId = Int32.Parse(m[1].Value);
            var vote =
                Uow.VoteRepository.GetAll().Where(v => v.WishListId == wishListId).FirstOrDefault(l => l.UserId == userId);
            var lastGiftId = 0;
            if (vote != null)
            {
                lastGiftId = vote.GiftId;
                Uow.VoteRepository.Delete(vote.Id);
                if (vote.GiftId != giftId)
                {
                    Uow.VoteRepository.Insert(new Vote() { GiftId = giftId, UserId = userId, WishListId = wishListId });
                }
            }
            else
            {
                Uow.VoteRepository.Insert(new Vote() {  GiftId = giftId, UserId = userId, WishListId = wishListId});
            }
            Uow.Commit();
            var mas = new List<int>
            {
                Uow.VoteRepository.GetAll().Where(l => l.GiftId == giftId).Count(v => v.WishListId == wishListId),
                lastGiftId, 
                wishListId,
                Uow.VoteRepository.GetAll().Where(l => l.GiftId == lastGiftId).Count(v => v.WishListId == wishListId)
            };
            return mas;
        }

        public int GetVotesCount(string wishListId, string giftId)
        {
            var intWishListId = Int32.Parse(wishListId);
            var intGiftId = Int32.Parse(giftId);
            return
                Uow.VoteRepository.GetAll()
                    .Where(v => v.WishListId == intWishListId).Count(v => v.GiftId == intGiftId);
        }

        public bool CheckCurrentUserInWishList(int id, int wishListId)
        {
            var wishList = Get(wishListId);
            var friend = wishList.Friends.Where(u => u.FriendId == id);
            return friend.Count() != 0;
        }
    }
}
