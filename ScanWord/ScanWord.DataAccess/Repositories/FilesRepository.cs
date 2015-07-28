using System.Data.Entity;
using ScanWord.Core.DataAccess.Repositories;
using ScanWord.Core.Entity;
using ScanWord.DataAccess.Repositories.Generic;

namespace ScanWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for files.</summary>
    public class FilesRepository : EfGenericRepository<File, int>, IFilesRepository
    {
        /// <summary>Initializes a new instance of the <see cref="FilesRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public FilesRepository(DbContext context) : base(context)
        {
        }
    }
}