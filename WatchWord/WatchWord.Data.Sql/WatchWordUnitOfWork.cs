using System;
using System.Data.Entity;
using System.Threading.Tasks;
using WatchWord.Data.Sql.Repositories;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.DataAccess.Repositories;

namespace WatchWord.Data.Sql
{
    public class WatchWordUnitOfWork : IWatchWordUnitOfWork, IDisposable
    {
        private DbContext _context;

        /// <summary>Initialises the new instance of the <see cref="WatchWordUnitOfWork"/></summary>
        /// <param name="context">The context.</param>
        public WatchWordUnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <summary>Gets the instance of the <see cref="IAccountsRepository/>.</summary>
        public IAccountsRepository AccountsRepository
        {
            get
            {
                return new AccountsRepository(_context);
            }
        }

        /// <summary>Gets the instance of the <see cref="IMaterialsRepository"/>.</summary>
        public IMaterialsRepository MaterialsRepository
        {
            get
            {
                return new MaterialsRepository(_context);
            }
        }

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The count of changed elements.</returns>
        public int Commit()
        {
            return _context.SaveChanges();
        }

        /// <summary>Saves all pending changes async.</summary>
        /// <returns>The count of changed elements.</returns>
        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        /// <summary>Disposes the current object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Disposes all external resources.</summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
