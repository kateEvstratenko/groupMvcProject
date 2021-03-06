﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class GiftService : BaseService, IGiftService
    {
        public GiftService(IUnitOfWork uow) : base(uow) { }

        public void Create(DomainGift domainGift)
        {
            var gift = Mapper.Map<Gift>(domainGift);
            Uow.GiftRepository.Insert(gift);
            Uow.Commit();
        }

        public void Delete(int id)
        {
            Uow.GiftRepository.Delete(id);
            Uow.Commit();
        }

        public void Update(DomainGift domainGift)
        {
            var gift = Mapper.Map<Gift>(domainGift);
            Uow.GiftRepository.Update(gift);
            Uow.Commit();
        }

        public DomainGift Get(int id)
        {
            var gift = Uow.GiftRepository.Get(id);
            if (gift == null)
            {
                return null;
            }
            var domainGift = Mapper.Map<DomainGift>(gift);
            return domainGift;
        }

        public IQueryable<DomainGift> GetAll()
        {
            var gifts = Uow.GiftRepository.GetAll();
            var domainGifts = gifts.Select(Mapper.Map<Gift, DomainGift>);
            return domainGifts.AsQueryable();
        }

        public IQueryable<DomainGift> GetPolular(int count)
        {
            var gifts = Uow.GiftRepository.GetAll();
            var domainGifts = gifts.Select(Mapper.Map<Gift, DomainGift>);
            return domainGifts.OrderByDescending(x => x.LikesCount).ToList().Take(count).AsQueryable();
        }

        public int ChangeLikesCount(string id, int userId)
        {
            var getId = new Regex("[0-9]+");
            var m = getId.Match(id);
            var gift = Uow.GiftRepository.Get(Int32.Parse(m.Value));
            var like =
                Uow.LikeRepository.GetAll().Where(l => l.GiftId == gift.Id).FirstOrDefault(l => l.UserId == userId);
            if (like != null)
            {
                gift.LikesCount--;
                Uow.LikeRepository.Delete(like.Id);
            }
            else
            {
                gift.LikesCount++;
                Uow.LikeRepository.Insert(new Like() { GiftId = gift.Id, UserId = userId });
            }
            Uow.GiftRepository.Update(gift);
            Uow.Commit();
            return gift.LikesCount;
        }

        public int ChangeViewsCount(int giftId, int userId)
        {
            var gift = Uow.GiftRepository.Get(giftId);
            var views =
                Uow.ViewRepository.GetAll().Where(l => l.GiftId == giftId).FirstOrDefault(l => l.UserId == userId);
            if (views == null)
            {
                gift.ViewsCount++;
                Uow.ViewRepository.Insert(new View() {GiftId = giftId, UserId = userId});
                Uow.GiftRepository.Update(gift);
                Uow.Commit();
            }
            return gift.ViewsCount;
        }

        public bool HaveWishlists(int userId)
        {
            var f = Uow.WishListRepository.GetAll().Where(x => x.UserId == userId).ToList();
            return (f.Count != 0);
        }

        public IQueryable<DomainGift> SearchGiftsByName(string namePart)
        {
            return GetAll().Where(u => u.Name.ToLower().Contains(namePart.ToLower()));
        }
    }
}
