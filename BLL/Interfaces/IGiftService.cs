using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IGiftService
    {
        void Create(DomainGift domainGift);
        void Delete(int id);
        void Update(DomainGift domainGift);
        DomainGift Get(int id);
        IQueryable<DomainGift> GetAll();
        IQueryable<DomainGift> GetPolular(int count);
    }
}
