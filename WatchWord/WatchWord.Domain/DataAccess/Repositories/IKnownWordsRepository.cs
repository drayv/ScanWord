using ScanWord.Core.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Domain.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for vocabulary of known words.</summary>
    public interface IKnownWordsRepository : IGenericRepository<KnownWord, int>
    {
    }
}