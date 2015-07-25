using System.Data.Entity;
using ScanWord.Core.Entity;
using ScanWord.Core.Data.Repositories;
using ScanWord.Data.Sql.Repositories.Generic;

namespace ScanWord.Data.Sql.Repositories
{
    class FilesRepository : ScanGenericRepository<File, int>, IFilesRepository
    {
        public FilesRepository(DbContext context) : base(context)
        {
        }
    }
}