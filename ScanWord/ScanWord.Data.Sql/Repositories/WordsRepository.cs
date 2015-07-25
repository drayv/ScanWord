using System.Data.Entity;
using ScanWord.Core.Data.Repositories;
using ScanWord.Core.Entity;
using ScanWord.Data.Sql.Repositories.Generic;

namespace ScanWord.Data.Sql.Repositories
{
    class WordsRepository : ScanGenericRepository<Word, int>, IWordsRepository
    {
        public WordsRepository(DbContext context) : base(context)
        {
        }
    }
}