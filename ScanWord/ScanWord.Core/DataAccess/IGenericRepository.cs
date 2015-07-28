using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ScanWord.Core.Entity.Common;

namespace ScanWord.Core.DataAccess
{
    /// <summary>Generic repository for entities.</summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TId">Type of entity Id.</typeparam>
    public interface IGenericRepository<TEntity, in TId> : IDisposable where TEntity : Entity<TId>
    {
        /// <summary>Inserts or updates entity.</summary>
        /// <param name="entity">The entity.</param>
        void InsertOrUpdate(TEntity entity);

        /// <summary>Inserts entities.</summary>
        /// <param name="entities">The entities enumerable.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>Gets entities from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Gets entities from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Finds entity by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The operation <see cref="TEntity"/>.</returns>
        TEntity GetById(TId id);

        /// <summary>Updates entity in database.</summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        void Update(TEntity entityToUpdate);

        /// <summary>Deletes entity from database by id.</summary>
        /// <param name="id">Entity id.</param>
        void Delete(TId id);

        /// <summary>Deletes entity from database.</summary>
        /// <param name="entityToDelete">Entity to delete.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>Deletes entities from database.</summary>
        /// <param name="entitiesToDelete">Entities to delete.</param>
        void Delete(IEnumerable<TEntity> entitiesToDelete);

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        int Save();

        /// <summary>Saves all pending changes asynchronously.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        Task<int> SaveAsync();
    }
}