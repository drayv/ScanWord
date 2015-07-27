using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WatchWord.Domain.DataAccess.Repositories.Generic;
using WatchWord.Domain.Entity.Common;

namespace WatchWord.Data.Sql.Repositories.Generic
{

    public abstract class WatchWordGenericRepository<TEntity, TType> : IGenericRepository<TEntity, TType>, IDisposable where TEntity : Entity<TType>
    {
        protected  DbContext _context;
        private DbSet<TEntity> _set;

        /// <summary>Creates the instance of the <see cref="WatchWordGenericRepository{TEntity, TType}"/>.</summary>
        /// <param name="context">The context</param>
        public WatchWordGenericRepository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        /// <summary>Deletes the element.</summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _set.Attach(entity);
            }
            _set.Remove(entity);
        }

        /// <summary>Gets the list of elements.</summary>
        /// <param name="whereProperties">The where predicate.</param>
        /// <param name="includeProperties">Which properties must be icluded.</param>
        /// <returns>The list of finded elements.</returns>
        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> whereProperties = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FilterQuery(_set.AsQueryable(), includeProperties, whereProperties).ToList();
        }

        /// <summary>Gets the list of elements async.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Which properties must be icluded.</param>
        /// <returns>The list of finded elements.</returns>
        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> whereProperties = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await FilterQuery(_set.AsQueryable(), includeProperties, whereProperties).ToListAsync();
        }

        /// <summary>Gets all elements.</summary>
        /// <returns>The list of entities.</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _set.ToList();
        }

        /// <summary>Finds the element by id.</summary>
        /// <param name="id">The id</param>
        /// <returns>The element or null if does not find it.</returns>
        public TEntity GetById(TType id)
        {
            return _set.Find(id);
        }

        /// <summary>Inserts the element.</summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        /// <summary>Adds the entity if it is does not exist otherwise update it.</summary>
        /// <param name="entity">The entity.</param>
        public virtual void InsertOrUpdate(TEntity entity)
        {
            _context.Entry(entity).State = entity.Id.Equals(default(TType)) ? EntityState.Added : EntityState.Modified;
        }

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The count of changed elements.</returns>
        public virtual int Save()
        {
            return _context.SaveChanges();
        }

        /// <summary>Saves all pending changes async.</summary>
        /// <returns>The count of changed elements.</returns>
        public virtual Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        /// <summary>Updates the element.</summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>Disposes the current object.</summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Filteres the query.</summary>
        /// <param name="whereProperties">The where predicate</param>
        /// <param name="includeProperties">Properties which must be included.</param>
        /// <param name="whereProperties"></param>
        /// <returns>The filtered query.</returns>
        private IQueryable<TEntity> FilterQuery(IQueryable<TEntity> rawQuery, Expression<Func<TEntity, object>>[] includeProperties, Expression<Func<TEntity, bool>> whereProperties = null)
        {
            includeProperties.Aggregate(rawQuery, (n, p) => n.Include(p));

            if (whereProperties != null)
            {
                rawQuery = rawQuery.Where(whereProperties);
            }

            return rawQuery;
        }

        /// <summary>Disposes all external resources.</summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }                      
        }
    }
}
