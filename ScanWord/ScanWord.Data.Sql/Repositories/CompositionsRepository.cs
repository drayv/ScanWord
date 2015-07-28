using System.Data.Entity;
using ScanWord.Core.Entity;
using ScanWord.Data.Sql.Repositories.Generic;

namespace ScanWord.Data.Sql.Repositories
{
    /// <summary>Represents repository pattern for compositions.</summary>
    public class CompositionsRepository : EFGenericRepository<Composition, int>, ICompositionsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CompositionsRepository{Composition,int}"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public CompositionsRepository(DbContext context) : base(context)
        {
        }
    }
}