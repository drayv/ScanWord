using ScanWord.Core.Data.Repositories.Generic;
using ScanWord.Core.Entity;

namespace ScanWord.Core.Data.Repositories
{
    /// <summary>Represents repository pattern for words.</summary>
    public interface IWordsRepository : IScanGenericRepository<Word, int>
    {
    }
}