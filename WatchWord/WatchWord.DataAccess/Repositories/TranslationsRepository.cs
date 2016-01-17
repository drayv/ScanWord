using System.Data.Entity;
using ScanWord.DataAccess.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for settings translations cache.</summary>
    public class TranslationsRepository : EfGenericRepository<Translation, int>, ITranslationsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="TranslationsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public TranslationsRepository(DbContext context) : base(context)
        {
        }
    }
}