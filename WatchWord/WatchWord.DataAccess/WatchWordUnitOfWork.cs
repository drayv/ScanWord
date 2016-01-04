using System;
using System.Data.Entity;
using System.Threading.Tasks;
using ScanWord.Core.DataAccess.Repositories;
using ScanWord.DataAccess.Repositories;
using WatchWord.DataAccess.Repositories;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.DataAccess.Repositories;

namespace WatchWord.DataAccess
{
    /// <summary>Represents unit of work pattern over WatchWord repositories.</summary>
    public class WatchWordUnitOfWork : IWatchWordUnitOfWork, IDisposable
    {
        private DbContext _context;

        /// <summary>Initialises the new instance of the <see cref="WatchWordUnitOfWork"/></summary>
        /// <param name="context">The context.</param>
        public WatchWordUnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <summary>Gets the instance of the <see cref="IWordsRepository"/>.</summary>
        public IWordsRepository WordsRepository
        {
            get { return new WordsRepository(_context); }
        }

        /// <summary>Gets the instance of the <see cref="IFilesRepository"/>.</summary>
        public IFilesRepository FilesRepository
        {
            get { return new FilesRepository(_context); }
        }

        /// <summary>Gets the instance of the <see cref="IMaterialsRepository"/>.</summary>
        public IMaterialsRepository MaterialsRepository
        {
            get { return new MaterialsRepository(_context); }
        }

        /// <summary>Gets the instance of the <see cref="IAccountsRepository"/>.</summary>
        public IAccountsRepository AccountsRepository
        {
            get { return new AccountsRepository(_context); }
        }

        /// <summary>Gets the instance of the <see cref="IKnownWordsRepository"/>.</summary>
        public IKnownWordsRepository KnownWordsRepository
        {
            get { return new KnownWordsRepository(_context); }
        }

        /// <summary>Gets the instance of the <see cref="ILearnWordsRepository"/>.</summary>
        public ILearnWordsRepository LearnWordsRepository
        {
            get { return new LearnWordsRepository(_context); }
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
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }
    }
}