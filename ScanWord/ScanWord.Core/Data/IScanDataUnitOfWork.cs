using System;
using System.Threading.Tasks;
using ScanWord.Core.Data.Repositories;

namespace ScanWord.Core.Data
{
    /// <summary>
    /// Represents unit of work pattern for ScanWord repositories.
    /// </summary>
    public interface IScanDataUnitOfWork : IDisposable
    {
        /// <summary>Gets compositions repository with unique context.</summary>
        /// <returns>Compositions repository.</returns>
        ICompositionsRepository CompositionsRepository();

        /// <summary>Gets files repository with unique context.</summary>
        /// <returns>Files repository.</returns>
        IFilesRepository FilesRepository();

        /// <summary>Gets words repository with unique context.</summary>
        /// <returns>Words repository.</returns>
        IWordsRepository WordsRepository();

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        int Commit();

        /// <summary>Saves all pending changes asynchronously.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        Task<int> CommitAsync();
    }
}