using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using DAL.Models;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal IDbSet<T> DbSet;
        internal IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim> Context;
        public Repository(IDbSet<T> dbSet, IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim> context)
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