using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyERP.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyERP.Application.Repositories
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;

        EntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token = default);

        void Dispose();

    }
}
