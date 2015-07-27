using System.IO;
using System.Text;
using System.Threading.Tasks;
using ScanWord.Core.Abstract;
using ScanWord.Core.Data;
using WatchWord.Domain.Common;
using WatchWord.Domain.Entity;
using File = ScanWord.Core.Entity.File;
using WatchWord.Domain.DataAccess;

namespace WatchWord.Service.EntityServices
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IWatchWordUnitOfWork _watchWordRepositories;
        private readonly IScanDataUnitOfWork _scanRepositories;
        private readonly IScanWordParser _parser;

        /// <summary>Prevents a default instance of the <see cref="MaterialsService"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private MaterialsService()
        {
        }

        public MaterialsService(IWatchWordUnitOfWork watchWordRepositories, IScanDataUnitOfWork scanRepositories, IScanWordParser parser)
        {
            _watchWordRepositories = watchWordRepositories;
            _scanRepositories = scanRepositories;
            _parser = parser;
        }

        public Material CreateMaterial(Stream stream, MaterialType type, string name, string description, int userId)
        {
            //TODO: this material exist
            var material = new Material { Description = description, Name = name, Type = type };

            using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("Windows-1251")))
            {
                var file = new File { Path = userId.ToString(), Filename = name, Extension = type.ToString(), FullName = userId + name + type };
                material.Words = _parser.ParseUnigueWordsInFile(file, streamReader);
                material.File = file;
            }

            return material;
        }

        public async Task<int> SaveMaterial(Material material)
        {
            _scanRepositories.WordsRepository().Insert(material.Words);
            await _scanRepositories.CommitAsync();

            _watchWordRepositories.MaterialsRepository.Insert(material);
            return await _watchWordRepositories.CommitAsync();
        }
    }
}