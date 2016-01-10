using ScanWord.Core.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Domain.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for settings.</summary>
    public interface ISettingsRepository : IGenericRepository<Setting, int>
    {
    }
}