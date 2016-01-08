using System.Data.Entity;
using ScanWord.DataAccess.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for materials.</summary>
    public class MaterialsRepository : EfGenericRepository<Material, int>, IMaterialsRepository
    {
        /// <summary>Entity framework context.</summary>
        private readonly DbContext _context;

        /// <summary>Initializes a new instance of the <see cref="MaterialsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public MaterialsRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        //TODO: fix generic repo to work with related entities
        public override void Insert(Material entity)
        {
            _context.Set<Material>().Add(entity);
            if (entity.Owner.Id != 0)
            {
                _context.Set<Account>().Attach(entity.Owner);
            }
        }
    }
}