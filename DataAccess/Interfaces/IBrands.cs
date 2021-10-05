using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Models.Entities.RemoteApi;

namespace DataAccess.Interfaces
{
    public interface IBrands
    {
        Task<Brand> GetAsync(int id, CancellationToken token);
        Task<Brand> GetAsync(string slug, CancellationToken token);
        Task<IEnumerable<Brand>> ListAsync(CancellationToken token);
        Task<IEnumerable<Brand>> ListAsync(Expression<Func<Brand, bool>> predicate, CancellationToken token);
        Task InsertAsync(Brand entity, CancellationToken token);
        Task UpdateAsync(Brand entity, CancellationToken token);
        Task DeleteAsync(Brand entity, CancellationToken token);
        Task InsertIfNotExistsAsync(Brand entity, CancellationToken token);
        Task UpdateOrInsertAsync(Brand entity, CancellationToken token);
    }
}