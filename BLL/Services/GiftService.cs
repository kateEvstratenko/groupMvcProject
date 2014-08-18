using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class GiftService: BaseService
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
            var domainGift = Mapper.Map<DomainGift>(gift);
            return domainGift;
        }

        public IQueryable<DomainGift> GetAll()
        {
            var gifts = Uow.GiftRepository.GetAll();
            var domainGifts = Mapper.Map<IEnumerable<DomainGift>>(gifts);
            return domainGifts.AsQueryable();
        }

        public IQueryable<DomainGift> GetPopularGifts(int count)
        {
            var gifts = Uow.GiftRepository.GetAll();
            var domainGifts = Mapper.Map<IEnumerable<DomainGift>>(gifts);
            return domainGifts.OrderByDescending(x => x.LikesCount).ToList().Take(count).AsQueryable(); 
        } 
    }
}
