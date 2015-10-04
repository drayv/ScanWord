using System.Data.Entity;
using ScanWord.DataAccess.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for materials.</summary>
    public class MaterialsRepository : EfGenericRepository<Material, int>, IMaterialsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MaterialsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public MaterialsRepository(DbContext context) : base(context)
        {
        }
    }
}