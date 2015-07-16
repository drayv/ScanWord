using WatchWord.Domain.Entity;

namespace WatchWord.Domain.Data
{
    /// <summary>The WatchWordRepository interface.</summary>
    public interface IWatchDataRepository
    {
        /// <summary>Add the account to database.</summary>
        /// <param name="account">The account.</param>
        void AddAccount(Account account);

        /// <summary>Add the material to database.</summary>
        /// <param name="material">The material.</param>
        void AddMaterial(Material material);
    }
}