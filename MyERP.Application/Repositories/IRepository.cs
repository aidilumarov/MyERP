using MyERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<List<T>> ListAsync();

        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task AddAsync(params T[] entities);

        Task DeleteAsync(T entity);

        Task DeleteAsync(params T[] entities);

        Task EditAsync(T entity);

        Task SaveAsync();

    }
}
