using ScanWord.Core.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Domain.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for vocabulary of learning words.</summary>
    public interface ILearnWordsRepository : IGenericRepository<LearnWord, int>
    {
    }
}