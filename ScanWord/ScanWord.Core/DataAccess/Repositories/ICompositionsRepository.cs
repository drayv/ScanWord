using ScanWord.Core.Entity;

namespace ScanWord.Core.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for compositions.</summary>
    public interface ICompositionsRepository : IGenericRepository<Composition, int>
    {
    }
}