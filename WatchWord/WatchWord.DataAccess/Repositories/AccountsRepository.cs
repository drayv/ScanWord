using System.Data.Entity;
using ScanWord.DataAccess.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for accounts.</summary>
    public class AccountsRepository : EfGenericRepository<Account, int>, IAccountsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AccountsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public AccountsRepository(DbContext context) : base(context)
        {
        }
    }
}