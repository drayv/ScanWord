using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ScanWord.Core.Entity.Common;

namespace ScanWord.Core.Data.Repositories.Generic
{
    /// <summary>Generic repository for entities.</summary>
    /// <typeparam name="TE">Type of entity.</typeparam>
    /// <typeparam name="TI">Type of entity Id.</typeparam>
    public interface IScanGenericRepository<TE, in TI> : IDisposable where TE : Entity<TI>
    {
        /// <summary>Insert or update entity.</summary>
        /// <param name="entity">The entity.</param>
        void InsertOrUpdate(TE entity);

        /// <summary>Insert entities.</summary>
        /// <param name="entities">The entities enumerable.</param>
        void Insert(IEnumerable<TE> entities);

        /// <summary>Get entities from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        Task<List<TE>> GetAllAsync(Expression<Func<TE, bool>> whereProperties = null,
            params Expression<Func<TE, object>>[] includeProperties);

        /// <summary>Get entities from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        List<TE> GetAll(Expression<Func<TE, bool>> whereProperties = null,
            params Expression<Func<TE, object>>[] includeProperties);

        /// <summary>Find entity by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The operation <see cref="TE"/>.</returns>
        TE GetById(TI id);

        /// <summary>Update entity in database.</summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        void Update(TE entityToUpdate);

        /// <summary>Deletes entity from database by id.</summary>
        /// <param name="id">Entity id.</param>
        void Delete(TI id);

        /// <summary>Deletes entity from database.</summary>
        /// <param name="entityToDelete">Entity to delete.</param>
        void Delete(TE entityToDelete);

        /// <summary>Deletes entities from database.</summary>
        /// <param name="entitiesToDelete">Entities to delete.</param>
        void Delete(IEnumerable<TE> entitiesToDelete);

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        int Save();

        /// <summary>Saves all pending changes asynchronously.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        Task<int> SaveAsync();
    }
}