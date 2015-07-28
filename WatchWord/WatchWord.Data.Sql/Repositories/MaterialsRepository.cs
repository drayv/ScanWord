using ScanWord.Data.Sql.Repositories.Generic;
using System.Data.Entity;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql.Repositories
{
    public class MaterialsRepository : EFGenericRepository<Material, int>, IMaterialsRepository
    {
        public MaterialsRepository(DbContext context) : base(context) { }
    }
}
