using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IFriendService
    {
        void Create(DomainFriend domainFriend);
        void Delete(int id);
        void Update(DomainFriend domainFriend);
        DomainFriend Get(int id);
        IQueryable<DomainFriend> GetAll();
    }
}
