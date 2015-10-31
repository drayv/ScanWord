using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ScanWord.Core.Entity.Common;

namespace ScanWord.Core.DataAccess
{
    /// <summary>Generic repository for entities.</summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TIdentity">Type of entity Id.</typeparam>
    public interface IGenericRepository<TEntity, in TIdentity> : IDisposable where TEntity : Entity<TIdentity>
    {
        /// <summary>Inserts the entity.</summary>
        /// <param name="entity">The entity.</param>
        void Insert(TEntity entity);

        /// <summary>Inserts entities.</summary>
        /// <param name="entities">The entities enumerable.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>Gets count of entities from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        int GetCount(Expression<Func<TEntity, bool>> whereProperties = null);

        /// <summary>Gets the entities asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Gets the entities.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Skip the given number and returns the specified number of entities from database asynchronously.</summary>
        /// <param name="skipNumber">Number of entities to skip.</param>
        /// <param name="amount">Number of entities to take.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        Task<List<TEntity>> SkipAndTakeAsync(int skipNumber, int amount,
            Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Skip the given number and returns the specified number of entities from database.</summary>
        /// <param name="skipNumber">Number of entities to skip.</param>
        /// <param name="amount">Number of entities to take.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        List<TEntity> SkipAndTake(int skipNumber, int amount, Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Finds entity by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The operation <see cref="TEntity"/>.</returns>
        TEntity GetById(TIdentity id);

        /// <summary>Gets the first entity which matchs the condition.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        TEntity GetByСondition(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Updates the entity.</summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        void Update(TEntity entityToUpdate);

        /// <summary>Deletes the entity by id.</summary>
        /// <param name="id">Entity id.</param>
        void Delete(TIdentity id);

        /// <summary>Deletes the entity.</summary>
        /// <param name="entityToDelete">Entity to delete.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>Deletes the entities.</summary>
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