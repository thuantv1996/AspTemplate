using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly WebTemplateContext _context;
        private DbSet<T> _dbSet;

        public Repository(WebTemplateContext context)
        {
            _context = context;
        }

        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _context.Set<T>());
        }

        public void Add(T entity)
        {
            if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
            {
                (entity as BaseEntity).CreatedDate = DateTime.UtcNow;
            }
            DbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
            {
                (entity as BaseEntity).CreatedDate = DateTime.UtcNow;
            }
            await DbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
            {
                (entity as BaseEntity).ModifyDate = DateTime.UtcNow;
            }
            DbSet.Update(entity);
        }

        public void Delete(object id, string user = "")
        {
            T entity = DbSet.Find(id);
            if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
            {
                if(entity != null)
                {
                    (entity as BaseEntity).ModifyDate = DateTime.UtcNow;
                    (entity as BaseEntity).ModifyBy = user;
                    (entity as BaseEntity).IsActive = false;
                    DbSet.Update(entity);
                }
            }
            else
            {
                DbSet.Remove(entity);
            }
        }

        public T FindById(int id)
        {
            T entity = DbSet.Find(id);
            return entity;
        }

        public async Task<T> FindByIdAsync(int id)
        {
            T entity = await DbSet.FindAsync(id);
            return entity;
        }

        public IQueryable<T> Select(Expression<Func<T, bool>> expr)
        {
            var query = DbSet.Where(expr);
            return query;
        }

        public IQueryable<T> SelectAll()
        {
            var query = DbSet.Select(s=>s);
            return query;
        }
    }
}
