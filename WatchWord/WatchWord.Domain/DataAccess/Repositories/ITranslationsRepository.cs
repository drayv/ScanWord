using ScanWord.Core.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Domain.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for settings translations cache.</summary>
    public interface ITranslationsRepository : IGenericRepository<Translation, int>
    {
    }
}