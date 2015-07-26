using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ScanWord.Core.Data.Repositories.Generic;
using ScanWord.Core.Entity.Common;

namespace ScanWord.Data.Sql.Repositories.Generic
{
    /// <summary>Generic repository for entities.</summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TId">Type of entity Id.</typeparam>
    public abstract class ScanGenericRepository<TEntity, TId> : IScanGenericRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        private DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>Initializes a new instance of the <see cref="ScanGenericRepository{TEntity,TId}"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        protected ScanGenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region CREATE

        /// <summary>Inserts or updates entity.</summary>
        /// <param name="entity">The entity.</param>
        public virtual void InsertOrUpdate(TEntity entity)
        {
            _context.Entry(entity).State = entity.Id.Equals(default(TId)) ? EntityState.Added : EntityState.Modified;
        }

        /// <summary>Inserts entities.</summary>
        /// <param name="entities">The entities enumerable.</param>
        public void Insert(IEnumerable<TEntity> entities)
        {
            _context.Configuration.AutoDetectChangesEnabled = false;
            _dbSet.AddRange(entities);
            _context.ChangeTracker.DetectChanges();
        }

        #endregion

        #region READ

        /// <summary>Gets entities from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await MakeQuery(_dbSet.AsNoTracking(), whereProperties, includeProperties).ToListAsync();
        }

        /// <summary>Gets entities from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return MakeQuery(_dbSet.AsNoTracking(), whereProperties, includeProperties).ToList();
        }

        /// <summary>Finds entity by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The operation <see cref="TEntity"/>.</returns>
        public virtual TEntity GetById(TId id)
        {
            return _dbSet.Find(id);
        }

        #endregion

        #region UPDATE

        /// <summary>Updates entity in database.</summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        #endregion

        #region DELETE

        /// <summary>Deletes entity from database by id.</summary>
        /// <param name="id">Entity id.</param>
        public virtual void Delete(TId id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>Deletes entity from database.</summary>
        /// <param name="entityToDelete">Entity to delete.</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            _dbSet.Remove(entityToDelete);
        }

        /// <summary>Deletes entities from database.</summary>
        /// <param name="entitiesToDelete">Entities to delete.</param>
        public virtual void Delete(IEnumerable<TEntity> entitiesToDelete)
        {
            _context.Configuration.AutoDetectChangesEnabled = false;
            _dbSet.RemoveRange(entitiesToDelete);
            _context.ChangeTracker.DetectChanges();
        }

        #endregion

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        public virtual int Save()
        {
            return _context.SaveChanges();
        }

        /// <summary>Saves all pending changes asynchronously.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>Makes query to table by using "where" predicate and "include(join) properties".</summary>
        /// <typeparam name="TSource">Entity type.</typeparam>
        /// <param name="table">Entity framework table.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns></returns>
        private static IQueryable<TSource> MakeQuery<TSource>(IQueryable<TSource> table, Expression<Func<TSource, bool>> whereProperties = null,
            params Expression<Func<TSource, object>>[] includeProperties) where TSource : Entity<TId>
        {
            //TODO: IOrderedQueryable param
            var query = includeProperties.Aggregate(table, (current, includeProperty) => current.Include(includeProperty));

            if (whereProperties != null)
            {
                query = query.Where(whereProperties);
            }

            return query;
        }

        /// <summary>Disposes the current object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Disposes all external resources.</summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }
    }
}