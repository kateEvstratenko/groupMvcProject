
﻿using System;
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
            if (entity != null)
            {
                DbSet.Add(entity);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id);
            if (entity != null)
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                DbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                throw new Exception();
            }
        }

        public T Get(int id)
        {
            var entity = DbSet.Find(id);
            if (entity != null)
            {
                return entity;
            }
            return null;
        }

        public IQueryable<T> GetAll()
        {
            var entities = DbSet;
            if (entities != null)
            {
                return entities;
            }
            return null;
        }
    }
}