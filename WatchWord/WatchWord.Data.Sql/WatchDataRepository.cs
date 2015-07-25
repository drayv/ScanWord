using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WatchWord.Domain;
using WatchWord.Domain.Data;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql
{
    /// <summary>Provides CRUD operations for WatchWord database.</summary>
    public class WatchDataRepository : IWatchDataRepository
    {
        /// <summary>Gets or sets the database name.</summary>
        private readonly IWatchProjectSettings _settings;

        /// <summary>Initializes a new instance of the <see cref="WatchDataRepository"/> class.</summary>
        /// <param name="settings">The data base name.</param>
        public WatchDataRepository(IWatchProjectSettings settings)
        {
            _settings = settings;
        }

        /// <summary>Prevents a default instance of the <see cref="WatchDataRepository"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private WatchDataRepository()
        {
        }

        #region CREATE

        /// <summary>Add the account to database.</summary>
        /// <param name="account">The account.</param>
        public void AddAccount(Account account)
        {
            using (var db = new WatchDataContainer(_settings))
            {
                db.Accounts.Add(account);
                db.SaveChanges();
            }
        }

        /// <summary>Add the material to database.</summary>
        /// <param name="material">The material.</param>
        public async Task<int> AddMaterialAsync(Material material)
        {
            using (var db = new WatchDataContainer(_settings))
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Entry(material.File).State = EntityState.Unchanged;
                db.Entry(material).State = EntityState.Added;
                db.ChangeTracker.DetectChanges();
                return await db.SaveChangesAsync();
            }
        }

        #endregion
        
        #region READ

        /// <summary>Get accounts from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of accounts.</returns>
        public async Task<List<Account>> GetAccountsAsync(Expression<Func<Account, bool>> whereProperties,
            params Expression<Func<Account, object>>[] includeProperties)
        {
            using (var db = new WatchDataContainer(_settings))
            {
                return await MakeQuery(db.Accounts.AsNoTracking(), whereProperties, includeProperties).ToListAsync();
            }
        }

        /// <summary>Get accounts from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of accounts.</returns>
        public List<Account> GetAccounts(Expression<Func<Account, bool>> whereProperties,
            params Expression<Func<Account, object>>[] includeProperties)
        {
            using (var db = new WatchDataContainer(_settings))
            {
                return MakeQuery(db.Accounts.AsNoTracking(), whereProperties, includeProperties).ToList();
            }
        }

        /// <summary>Get materials from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of materials.</returns>
        public async Task<List<Material>> GetMaterialsAsync(Expression<Func<Material, bool>> whereProperties,
            params Expression<Func<Material, object>>[] includeProperties)
        {
            using (var db = new WatchDataContainer(_settings))
            {
                return await MakeQuery(db.Materials.AsNoTracking(), whereProperties, includeProperties).ToListAsync();
            }
        }

        /// <summary>Get materials from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of materials.</returns>
        public List<Material> GetMaterials(Expression<Func<Material, bool>> whereProperties,
            params Expression<Func<Material, object>>[] includeProperties)
        {
            using (var db = new WatchDataContainer(_settings))
            {
                return MakeQuery(db.Materials.AsNoTracking(), whereProperties, includeProperties).ToList();
            }
        }

        #endregion

        #region UPDATE
        #endregion

        #region DELETE
        #endregion

        /// <summary>Makes query to table by using "where" predicate and "include(join) properties".</summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="table">Entity framework table.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns></returns>
        private static IQueryable<TEntity> MakeQuery<TEntity>(IQueryable<TEntity> table, Expression<Func<TEntity, bool>> whereProperties,
            params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            var query = includeProperties.Aggregate(table, (current, includeProperty) => current.Include(includeProperty));
            query = query.Where(whereProperties);
            return query;
        }
    }
}