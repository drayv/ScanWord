using ScanWord.Core.Data.Repositories.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql.Repositories
{
    public interface IMaterialsRepository: IGenericRepository<Material, int>
    {
    }
}
