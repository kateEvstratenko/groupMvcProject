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

        public bool Create(int userId, int friendId)
        {
            if (
                Uow.FriendRepository.GetAll().Where(u => u.UserId == userId).FirstOrDefault(u => u.FriendId == friendId)
                != null)
            {
                return false;
            }

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
            return true;
        }

        public void Delete(int userId, int friendId)
        {
            var allFriends = Uow.FriendRepository.GetAll();
            var friend = allFriends.Where(f => f.UserId == userId).FirstOrDefault(f => f.FriendId == friendId);

            if (friend != null)
            {
                Uow.FriendRepository.Delete(friend.Id);
            }

            friend = allFriends.Where(f => f.UserId == friendId).FirstOrDefault(f => f.FriendId == userId);
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
            var domainFriends = new List<DomainUser>();
            foreach (var friend in friends)
            {
                domainFriends.Add(Mapper.Map<DomainUser>(Uow.UserRepository.Get(friend.FriendId)));
            }

            return domainFriends.AsQueryable();
        }

        public IQueryable<DomainFriend> GetAllFriends(int id)
        {
            var friends = Uow.FriendRepository.GetAll().Where(f => f.UserId == id);
            var domainFriends = friends.Select(Mapper.Map<Friend, DomainFriend>);

            return domainFriends.AsQueryable();
        } 
    }
}
