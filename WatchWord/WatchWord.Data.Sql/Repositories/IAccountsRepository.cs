using ScanWord.Core.Data.Repositories.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql.Repositories
{
    public interface IAccountsRepository: IGenericRepository<Account, int>
    {
    }
}
