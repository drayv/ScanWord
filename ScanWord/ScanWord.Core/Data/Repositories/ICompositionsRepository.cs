using ScanWord.Core.Data.Repositories.Generic;
using ScanWord.Core.Entity;

namespace ScanWord.Core.Data.Repositories
{
    /// <summary>Represents repository pattern for compositions.</summary>
    public interface ICompositionsRepository : IScanGenericRepository<Composition, int>
    {
    }
}