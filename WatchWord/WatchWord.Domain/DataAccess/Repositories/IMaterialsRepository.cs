using ScanWord.Core.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Domain.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for materials.</summary>
    public interface IMaterialsRepository: IGenericRepository<Material, int>
    {
    }
}