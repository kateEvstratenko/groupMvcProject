using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal IDbSet<T> DbSet;
        internal DbContext Context;
        public Repository(IDbSet<T> dbSet, DbContext context)
        {
            DbSet = dbSet;
            Context = context;
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id);
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public T Get(int id)
        {
            var result = DbSet.Find(id);
            
            return result;
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }
    }
}