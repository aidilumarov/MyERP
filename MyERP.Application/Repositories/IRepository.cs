using MyERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Application.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> ListAsync();

        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity, bool identityInsert);

        Task AddAsync(IEnumerable<T> entities, bool identityInsert);

        Task<int> DeleteAsync(T entity);

        Task<int> DeleteAsync(params T[] entities);

        Task<int> EditAsync(T entity);

        Task<int> SaveAsync();

    }
}
