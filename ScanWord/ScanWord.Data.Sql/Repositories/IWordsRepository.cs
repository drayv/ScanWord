using ScanWord.Core.Data.Repositories.Generic;
using ScanWord.Core.Entity;

namespace ScanWord.Data.Sql.Repositories
{
    /// <summary>Represents repository pattern for words.</summary>
    public interface IWordsRepository : IGenericRepository<Word, int>
    {
    }
}