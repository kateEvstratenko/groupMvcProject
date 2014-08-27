using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
