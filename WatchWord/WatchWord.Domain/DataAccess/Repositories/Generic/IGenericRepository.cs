using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WatchWord.Domain.Entity.Common;

namespace WatchWord.Domain.DataAccess.Repositories.Generic
{
    public interface IGenericRepository<TEntity, TType> where TEntity : IEntity<TType>
    {
        /// <summary>Finds the element by id.</summary>
        /// <param name="id">The id</param>
        /// <returns>The element or null if does not find it.</returns>
        TEntity GetById(TType id);

        /// <summary>Gets all elements.</summary>
        /// <returns>The list of entities.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>Gets the list of elements.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Which properties must be icluded.</param>
        /// <returns>The list of finded elements.</returns>
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Gets the list of elements async.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Which properties must be icluded.</param>
        /// <returns>The list of finded elements.</returns>
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Inserts the element.</summary>
        /// <param name="entity">The entity.</param>
        void Insert(TEntity entity);

        /// <summary>Adds the entity if it is does not exist otherwise update it.</summary>
        /// <param name="entity">The entity.</param>
        void InsertOrUpdate(TEntity entity);

        /// <summary>Updates the element.</summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>Deletes the element.</summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The count of changed elements.</returns>
        int Save();

        /// <summary>Saves all pending changes async.</summary>
        /// <returns>The count of changed elements.</returns>
        Task<int> SaveAsync();
    }
}
