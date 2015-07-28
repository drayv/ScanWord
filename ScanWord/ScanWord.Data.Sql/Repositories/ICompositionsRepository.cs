using ScanWord.Core.Data.Repositories.Generic;
using ScanWord.Core.Entity;

namespace ScanWord.Data.Sql.Repositories
{
    /// <summary>Represents repository pattern for compositions.</summary>
    public interface ICompositionsRepository : IGenericRepository<Composition, int>
    {
    }
}