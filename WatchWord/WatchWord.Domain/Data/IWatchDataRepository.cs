using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        Task<int> AddMaterialAsync(Material material);

        /// <summary>Get accounts from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of accounts.</returns>
        Task<List<Account>> GetAccountsAsync(Expression<Func<Account, bool>> whereProperties,
            params Expression<Func<Account, object>>[] includeProperties);

        /// <summary>Get accounts from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of accounts.</returns>
        List<Account> GetAccounts(Expression<Func<Account, bool>> whereProperties,
            params Expression<Func<Account, object>>[] includeProperties);

        /// <summary>Get materials from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of materials.</returns>
        Task<List<Material>> GetMaterialsAsync(Expression<Func<Material, bool>> whereProperties,
            params Expression<Func<Material, object>>[] includeProperties);

        /// <summary>Get materials from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of materials.</returns>
        List<Material> GetMaterials(Expression<Func<Material, bool>> whereProperties,
            params Expression<Func<Material, object>>[] includeProperties);
    }
}