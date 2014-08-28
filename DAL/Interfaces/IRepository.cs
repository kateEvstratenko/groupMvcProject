using System.Linq;
using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);
        T Get(int id);
        IQueryable<T> GetAll();
    }
}