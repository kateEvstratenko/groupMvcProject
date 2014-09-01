﻿using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        void Create(DomainComment domainTag, int userId, string kind);
        void Delete(int id);
        void Update(DomainComment domainTag);
        DomainComment Get(int id);
        IQueryable<DomainComment> GetAll();
        int GetLikesCount(int id);
        int ChangeLikesCount(string id, int userId);
    }
}
