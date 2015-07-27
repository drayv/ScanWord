using System.Data.Entity;
using WatchWord.Data.Sql.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql.Repositories
{
    public class AccountsRepository : WatchWordGenericRepository<Account, int>, IAccountsRepository
    {
        public AccountsRepository(DbContext context) : base(context)
        {

        }
    }
}
