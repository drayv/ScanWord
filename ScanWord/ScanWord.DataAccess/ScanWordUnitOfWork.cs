using System;
using System.Data.Entity;
using System.Threading.Tasks;
using ScanWord.Core.DataAccess;
using ScanWord.Core.DataAccess.Repositories;
using ScanWord.DataAccess.Repositories;

namespace ScanWord.DataAccess
{
    /// <summary>Represents unit of work pattern for ScanWord repositories.</summary>
    public class ScanWordUnitOfWork : IScanWordUnitOfWork
    {
        /// <summary>Entity framework context.</summary>
        private DbContext _dbContext;

        /// <summary>Initializes a new instance of the <see cref="ScanWordUnitOfWork"/> class.</summary>
        /// <param name="context">>Entity framework context.</param>
        public ScanWordUnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        //TODO: IoC in one lifetime scope with UnitOfWork for all repositories.

        /// <summary>Gets compositions repository with unique context.</summary>
        /// <returns>Compositions repository.</returns>
        public ICompositionsRepository CompositionsRepository()
        {
            return new CompositionsRepository(_dbContext);   
        }

        /// <summary>Gets files repository with unique context.</summary>
        /// <returns>Files repository.</returns>
        public IFilesRepository FilesRepository()
        {
            return new FilesRepository(_dbContext);
        }

        /// <summary>Gets words repository with unique context.</summary>
        /// <returns>Words repository.</returns>
        public IWordsRepository WordsRepository()
        {
            return new WordsRepository(_dbContext);
        }

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>Saves all pending changes asynchronously.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
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
            if (_dbContext == null) return;
            _dbContext.Dispose();
            _dbContext = null;
        }
    }
}