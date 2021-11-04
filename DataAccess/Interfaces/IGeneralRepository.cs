using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace DataAccess.Interfaces
{
    public interface IGeneralRepository<T>
    {
        Task<T> InsertAsync(T model,
            CancellationToken token);

        Task<T> InsertIfNotExistAsync(Expression<Func<T, bool>> condition, T model,
            CancellationToken token);

        Task<T> InsertOrUpdateAsync(Expression<Func<T, bool>> condition, T model,
            CancellationToken token);

        Task<T> RemoveAsync(T model,
            CancellationToken token);

        Task<T> UpdateAsync(T model,
            CancellationToken token);

        Task<List<T>> GetAllAsync(CancellationToken token);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> condition,
            CancellationToken token);

        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> condition, Expression<Func<T, TKey>> orderBy,
            CancellationToken token);

        Task<List<T>> GetAllIncludeAsync<TKey>(Expression<Func<T, bool>> condition, Expression<Func<T, TKey>> include,
            CancellationToken token);

        Task<T> GetOneAsync(Expression<Func<T, bool>> condition,
            CancellationToken token);

        Task<double?> AverageAsync(Expression<Func<T, bool>> condition, Expression<Func<T, int?>> selector,
            CancellationToken token);

        void DetachEntity(T model);
    }
}