using System.Threading.Tasks;
using WatchWord.Domain.DataAccess.Repositories;

namespace WatchWord.Domain.DataAccess
{
    public interface IWatchWordUnitOfWork
    {
        /// <summary>Gets the instance of the <see cref="IMaterialsRepository"/>.</summary>
        IMaterialsRepository MaterialsRepository { get; }

        /// <summary>Gets the instance of the <see cref="IAccountsRepository/>.</summary>
        IAccountsRepository AccountsRepository { get; }

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The count of changed elements.</returns>
        int Commit();

        /// <summary>Saves all pending changes async.</summary>
        /// <returns>The count of changed elements.</returns>
        Task<int> CommitAsync();
    }
}
