using System.Threading;
using System.Threading.Tasks;
using Models.Entities;

namespace DataAccess.Interfaces
{
    public interface IPhonesRep
    {
        Task<Phone> GetPhoneBySlugAsync(string slug, CancellationToken token);
        Task InsertAsync(Phone phone, CancellationToken token);
        Task UpdateAsync(Phone phone, CancellationToken token);
        Task InsertOrUpdateAsync(Phone phone, CancellationToken token);
    }
}