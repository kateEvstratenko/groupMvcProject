﻿using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IFriendService
    {
        bool Create(int userId, int friendId);
        void Delete(int userId, int friendId);
        void Update(DomainFriend domainFriend);
        DomainFriend Get(int id);
        IQueryable<DomainUser> GetAll(int id);
        IQueryable<DomainFriend> GetAllFriends(int id);
    }
}
