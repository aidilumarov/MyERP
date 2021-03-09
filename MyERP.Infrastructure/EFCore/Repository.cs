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
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IApplicationDbContext db;

        public Repository(IApplicationDbContext db)
        {
            this.db = db;
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await db.Set<T>().FindAsync(id).AsTask();
        }

        public async virtual Task<List<T>> ListAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity, bool identityInsert)
        {
            if (identityInsert)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    SetIdentityInsertOn();
                    await db.Set<T>().AddAsync(entity);
                    await transaction.CommitAsync();
                }
            }
            
            else
            {
                await db.Set<T>().AddAsync(entity);
            }

            await db.SaveChangesAsync();
            return entity;
        }

        public async Task AddAsync(IEnumerable<T> entities, bool identityInsert)
        {
            if (identityInsert)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    SetIdentityInsertOn();
                    await db.Set<T>().AddRangeAsync(entities);
                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            
            else
            {
                await db.Set<T>().AddRangeAsync(entities);
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(T entity)
        {
            db.Set<T>().Remove(entity);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(params T[] entities)
        {
            db.Set<T>().RemoveRange(entities);
            return await db.SaveChangesAsync();
        }

        public virtual async Task<int> EditAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return await db.SaveChangesAsync();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await db.SaveChangesAsync();
        }

        private void SetIdentityInsertOn()
        {
            db.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [dbo].[{typeof(T).Name}s] ON");
        }

        private void SetIdentityInsertOff()
        {
            db.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [dbo].[{typeof(T).Name}s] OFF");
        }

    }
}
