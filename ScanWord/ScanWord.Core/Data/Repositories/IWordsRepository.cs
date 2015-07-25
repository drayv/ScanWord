using ScanWord.Core.Data.Repositories.Generic;
using ScanWord.Core.Entity;

namespace ScanWord.Core.Data.Repositories
{
    public interface IWordsRepository : IScanGenericRepository<Word, int>
    {
    }
}