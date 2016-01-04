using System.Data.Entity;
using ScanWord.DataAccess.Repositories.Generic;
using WatchWord.Domain.DataAccess.Repositories;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for vocabulary of learning words.</summary>
    public class LearnWordsRepository : EfGenericRepository<LearnWord, int>, ILearnWordsRepository
    {
        private readonly DbContext _context;

        /// <summary>Initializes a new instance of the <see cref="LearnWordsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public LearnWordsRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        //TODO: fix generic repo to work with related entities
        public override void Insert(LearnWord entity)
        {
            _context.Set<LearnWord>().Add(entity);
            if (entity.Owner.Id != 0)
            {
                _context.Set<Account>().Attach(entity.Owner);
            }
        }
    }
}