using System.Data.Entity;
using ScanWord.Core.Entity;
using ScanWord.Data.Sql.Repositories.Generic;

namespace ScanWord.Data.Sql.Repositories
{
    /// <summary>Represents repository pattern for files.</summary>
    public class FilesRepository : EFGenericRepository<File, int>, IFilesRepository
    {
        /// <summary>Initializes a new instance of the <see cref="FilesRepository{File,int}"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public FilesRepository(DbContext context) : base(context)
        {
        }
    }
}