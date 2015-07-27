using System.Data.Entity;
using WatchWord.Data.Sql.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql.Repositories
{
    public class MaterialsRepository : WatchWordGenericRepository<Material, int>, IMaterialsRepository
    {
        public MaterialsRepository(DbContext context) : base(context) { }
    }
}
