using System.Data.Entity;
using ScanWord.DataAccess.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for settings.</summary>
    public class SettingsRepository : EfGenericRepository<Setting, int>, ISettingsRepository
    {
        /// <summary>Entity framework context.</summary>
        private readonly DbContext _context;

        /// <summary>Initializes a new instance of the <see cref="MaterialsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public SettingsRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        //TODO: fix generic repo to work with related entities
        public override void Insert(Setting entity)
        {
            _context.Set<Setting>().Add(entity);
            if (entity.Owner != null && entity.Owner.Id != 0)
            {
                _context.Set<Account>().Attach(entity.Owner);
            }
        }
    }
}