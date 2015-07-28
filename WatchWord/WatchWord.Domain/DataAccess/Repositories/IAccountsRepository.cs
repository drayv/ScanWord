using ScanWord.Core.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Domain.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for accounts.</summary>
    public interface IAccountsRepository: IGenericRepository<Account, int>
    {
    }
}