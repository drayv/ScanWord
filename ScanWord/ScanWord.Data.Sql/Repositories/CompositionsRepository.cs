using System.Data.Entity;
using ScanWord.Core.Data.Repositories;
using ScanWord.Core.Entity;
using ScanWord.Data.Sql.Repositories.Generic;

namespace ScanWord.Data.Sql.Repositories
{
    public class CompositionsRepository : ScanGenericRepository<Composition, int>, ICompositionsRepository
    {
        public CompositionsRepository(DbContext context) : base(context)
        {
        }
    }
}