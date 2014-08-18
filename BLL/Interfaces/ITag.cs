using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ITagService
    {
        void Create(DomainTag domainTag);
        void Delete(int id);
        void Update(DomainTag domainTag);
        DomainTag Get(int id);
        IQueryable<DomainTag> GetAll();
    }
}
