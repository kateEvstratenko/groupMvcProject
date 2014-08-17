using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class FriendService: BaseService, IService<DomainFriend>
    {
        public FriendService(IUnitOfWork uow) : base(uow) { }

        public void Create(DomainFriend domainFriend)
        {
            var friend = Mapper.Map<Friend>(domainFriend);
            Uow.FriendRepository.Insert(friend);   
            Uow.Commit();
        }

        public void Delete(int id)
        {
            Uow.FriendRepository.Delete(id);
            Uow.Commit();
        }

        public void Update(DomainFriend domainFriend)
        {
            var friend = Mapper.Map<Friend>(domainFriend);
            Uow.FriendRepository.Update(friend);
            Uow.Commit();
        }

        public DomainFriend Get(int id)
        {
            var friend = Uow.FriendRepository.Get(id);
            var domainFriend = Mapper.Map<DomainFriend>(friend);
            return domainFriend;
        }

        public IQueryable<DomainFriend> GetAll()
        {
            var friends = Uow.FriendRepository.GetAll();
            var domainFriends = Mapper.Map<IQueryable<DomainFriend>>(friends);
            return domainFriends;
        } 
    }
}
