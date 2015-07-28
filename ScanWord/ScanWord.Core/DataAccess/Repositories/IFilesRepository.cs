using ScanWord.Core.Entity;

namespace ScanWord.Core.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for files.</summary>
    public interface IFilesRepository : IGenericRepository<File, int>
    {
    }
}