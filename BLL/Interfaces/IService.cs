using System.Linq;

namespace BLL.Interfaces
{
    public interface IService<T> where T: class
    {
        void Create(T domainEntity);
        void Delete(int id);
        void Update(T domainEntity);
        T Get(int id);
        IQueryable<T> GetAll();
    }
}
