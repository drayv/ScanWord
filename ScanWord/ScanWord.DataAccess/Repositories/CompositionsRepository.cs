using System.Data.Entity;
using ScanWord.Core.DataAccess.Repositories;
using ScanWord.Core.Entity;
using ScanWord.DataAccess.Repositories.Generic;

namespace ScanWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for compositions.</summary>
    public class CompositionsRepository : EfGenericRepository<Composition, int>, ICompositionsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CompositionsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public CompositionsRepository(DbContext context) : base(context)
        {
        }
    }
}