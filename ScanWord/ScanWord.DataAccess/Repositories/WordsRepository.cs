using System.Data.Entity;
using ScanWord.Core.DataAccess.Repositories;
using ScanWord.Core.Entity;
using ScanWord.DataAccess.Repositories.Generic;

namespace ScanWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for words.</summary>
    public class WordsRepository : EfGenericRepository<Word, int>, IWordsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="WordsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public WordsRepository(DbContext context) : base(context)
        {
        }
    }
}