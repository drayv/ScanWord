using ScanWord.Data.Sql.Repositories.Generic;
using System.Data.Entity;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql.Repositories
{
    public class AccountsRepository : EFGenericRepository<Account, int>, IAccountsRepository
    {
        public AccountsRepository(DbContext context) : base(context)
        {

        }
    }
}
