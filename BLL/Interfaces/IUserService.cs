using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void Create(DomainUser domainUser);
        void Delete(int id);
        void Update(DomainUser domainUser);
        DomainUser Get(int id);
        IQueryable<DomainUser> GetAll();
    }
}
