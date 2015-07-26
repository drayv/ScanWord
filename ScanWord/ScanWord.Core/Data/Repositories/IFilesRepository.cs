using ScanWord.Core.Data.Repositories.Generic;
using ScanWord.Core.Entity;

namespace ScanWord.Core.Data.Repositories
{
    /// <summary>Represents repository pattern for files.</summary>
    public interface IFilesRepository : IScanGenericRepository<File, int>
    {
    }
}