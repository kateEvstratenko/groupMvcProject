using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class WishListService: BaseService, IService<DomainWishList>
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
            var wishList = Mapper.Map<WishList>(domainWishList);
            Uow.WishListRepository.Update(wishList);
            Uow.Commit();
        }

        public DomainWishList Get(int id)
        {
            var wishList = Uow.WishListRepository.Get(id);
            var domainWishList = Mapper.Map<DomainWishList>(wishList);
            return domainWishList;
        }

        public IQueryable<DomainWishList> GetAll()
        {
            var wishLists = Uow.WishListRepository.GetAll();
            var domainWishLists = Mapper.Map<IQueryable<DomainWishList>>(wishLists);
            return domainWishLists;
        } 
    }
}
