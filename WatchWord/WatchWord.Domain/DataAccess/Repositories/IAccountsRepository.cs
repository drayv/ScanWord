using WatchWord.Domain.DataAccess.Repositories.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Domain.DataAccess.Repositories
{
    public interface IAccountsRepository: IGenericRepository<Account, int>
    {
    }
}
