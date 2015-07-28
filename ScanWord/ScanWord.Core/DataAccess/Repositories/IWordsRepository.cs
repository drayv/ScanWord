using ScanWord.Core.Entity;

namespace ScanWord.Core.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for words.</summary>
    public interface IWordsRepository : IGenericRepository<Word, int>
    {
    }
}