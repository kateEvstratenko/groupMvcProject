using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class FriendService: BaseService, IFriendService
    {
        public FriendService(IUnitOfWork uow) : base(uow) { }

        public void Create(int userId, int friendId)
        {
            var friend = new Friend()
            {
                UserId = userId,
                FriendId = friendId
            };
            Uow.FriendRepository.Insert(friend);  

            friend = new Friend()
            {
                UserId = friendId,
                FriendId = userId
            };
            Uow.FriendRepository.Insert(friend);  
            
            Uow.Commit();
        }

        public void Delete(int userId, int friendId)
        {
            var allFriends = Uow.FriendRepository.GetAll();
            var friend = allFriends.Where(f => f.UserId == userId).SingleOrDefault(f => f.FriendId == friendId);

            if (friend != null)
            {
                Uow.FriendRepository.Delete(friend.Id);
            }

            friend = allFriends.Where(f => f.UserId == friendId).SingleOrDefault(f => f.FriendId == userId);
            if (friend != null)
            {
                Uow.FriendRepository.Delete(friend.Id);
            }
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

        public IQueryable<DomainUser> GetAll(int id)
        {
            var friends = Uow.FriendRepository.GetAll().Where(f => f.UserId == id).ToArray();
            var domainFriends = friends.Select(friend => Get(friend.FriendId).User).AsQueryable();

            return domainFriends;
        } 
    }
}
