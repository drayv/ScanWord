using System.Data.Entity;
using ScanWord.DataAccess.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for vocabulary of known words.</summary>
    public class KnownWordsRepository : EfGenericRepository<KnownWord, int>, IKnownWordsRepository
    {
        /// <summary>Entity framework context.</summary>
        private readonly DbContext _context;

        /// <summary>Initializes a new instance of the <see cref="KnownWordsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public KnownWordsRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        //TODO: fix generic repo to work with related entities
        public override void Insert(KnownWord entity)
        {
            _context.Set<KnownWord>().Add(entity);
            if (entity.Owner.Id != 0)
            {
                _context.Set<Account>().Attach(entity.Owner);
            }
        }
    }
}