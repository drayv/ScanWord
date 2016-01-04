using System.Threading.Tasks;
using ScanWord.Core.DataAccess.Repositories;
using WatchWord.Domain.DataAccess.Repositories;

namespace WatchWord.Domain.DataAccess
{
    /// <summary>Represents unit of work pattern over WatchWord repositories.</summary>
    public interface IWatchWordUnitOfWork
    {
        /// <summary>Gets the instance of the <see cref="IWordsRepository"/>.</summary>
        IWordsRepository WordsRepository { get; }

        /// <summary>Gets the instance of the <see cref="IFilesRepository"/>.</summary>
        IFilesRepository FilesRepository { get; }

        /// <summary>Gets the instance of the <see cref="IMaterialsRepository"/>.</summary>
        IMaterialsRepository MaterialsRepository { get; }

        /// <summary>Gets the instance of the <see cref="IAccountsRepository"/>.</summary>
        IAccountsRepository AccountsRepository { get; }

        /// <summary>Gets the instance of the <see cref="IKnownWordsRepository"/>.</summary>
        IKnownWordsRepository KnownWordsRepository { get; }

        /// <summary>Gets the instance of the <see cref="ILearnWordsRepository"/>.</summary>
        ILearnWordsRepository LearnWordsRepository { get; }

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The count of changed elements.</returns>
        int Commit();

        /// <summary>Saves all pending changes async.</summary>
        /// <returns>The count of changed elements.</returns>
        Task<int> CommitAsync();
    }
}