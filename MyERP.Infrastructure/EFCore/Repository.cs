using Microsoft.EntityFrameworkCore;
using MyERP.Application.Repositories;
using MyERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Infrastructure.EFCore
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IApplicationDbContext db;

        public Repository(IApplicationDbContext db)
        {
            this.db = db;
        }

        public virtual Task<T> GetByIdAsync(Guid id)
        {
            return db.Set<T>().FindAsync(id).AsTask();
        }

        public virtual Task<List<T>> ListAsync()
        {
            return db.Set<T>().ToListAsync();
        }

        public virtual Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            db.Set<T>().Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public Task AddAsync(params T[] entities)
        {
            db.Set<T>().AddRange(entities);
            return db.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            db.Set<T>().Remove(entity);
            return db.SaveChangesAsync();
        }

        public Task DeleteAsync(params T[] entities)
        {
            db.Set<T>().RemoveRange(entities);
            return db.SaveChangesAsync();
        }

        public virtual Task EditAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChangesAsync();
        }

        public virtual Task SaveAsync()
        {
            return db.SaveChangesAsync();
        }

    }
}
