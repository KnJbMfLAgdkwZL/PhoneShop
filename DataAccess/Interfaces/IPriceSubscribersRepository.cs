using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Database.Models;

namespace DataAccess.Interfaces
{
    public interface IPriceSubscribersRepository
    {
        Task<PriceSubscriber> GetOneAsync(Expression<Func<PriceSubscriber, bool>> predicate, CancellationToken token);
        Task InsertAsync(PriceSubscriber priceSubs, CancellationToken token);

        Task<List<PriceSubscriber>> GetAllAsync(Expression<Func<PriceSubscriber, bool>> predicate,
            CancellationToken token);
    }
}