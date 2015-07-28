using ScanWord.Core.Data.Repositories.Generic;
using ScanWord.Core.Entity;

namespace ScanWord.Data.Sql.Repositories
{
    /// <summary>Represents repository pattern for files.</summary>
    public interface IFilesRepository : IGenericRepository<File, int>
    {
    }
}