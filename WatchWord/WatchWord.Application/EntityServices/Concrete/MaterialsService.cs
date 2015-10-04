using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ScanWord.Core.Abstract;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.Entity;
using File = ScanWord.Core.Entity.File;

namespace WatchWord.Application.EntityServices.Concrete
{
    /// <summary>Represents a layer for work with materials.</summary>
    public class MaterialsService : IMaterialsService
    {
        private readonly IWatchWordUnitOfWork _watchWordUnitOfWork;
        private readonly IScanWordParser _parser;

        /// <summary>Prevents a default instance of the <see cref="MaterialsService"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private MaterialsService()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MaterialsService"/> class.</summary>
        /// <param name="watchWordUnitOfWork">Unit of work over WatchWord repositories.</param>
        /// <param name="parser">Words parser.</param>
        public MaterialsService(IWatchWordUnitOfWork watchWordUnitOfWork, IScanWordParser parser)
        {
            _watchWordUnitOfWork = watchWordUnitOfWork;
            _parser = parser;
        }

        /// <summary>Creates material with the specified attributes.</summary>
        /// <param name="stream">Subtitles file stream.</param>
        /// <param name="type">Material type <see cref="MaterialType"/>.</param>
        /// <param name="name">Name of the material.</param>
        /// <param name="description">Description of the material.</param>
        /// <param name="userId">Owner Id.</param>
        /// <returns>Created material by specific attributes.</returns>
        public Material CreateMaterial(Stream stream, MaterialType type, string name, string description, int userId)
        {
            //TODO: this material exist check
            var material = new Material { Description = description, Name = name, Type = type };

            using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("Windows-1251")))
            {
                var file = new File { Path = userId.ToString(), Filename = name, Extension = type.ToString(), FullName = userId + name + type };
                file.Words = _parser.ParseUnigueWordsInFile(file, streamReader);
                material.File = file;
            }

            return material;
        }

        /// <summary>Saves material to the data storage.</summary>
        /// <param name="material">The material <see cref="Material"/>.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        public async Task<int> SaveMaterial(Material material)
        {
            _watchWordUnitOfWork.MaterialsRepository.Insert(material);
            return await _watchWordUnitOfWork.CommitAsync();
        }

        /// <summary>Skip the given number and returns the specified number of materials from the data storage.</summary>
        /// <param name="startIndex">Number of materials to skip.</param>
        /// <param name="amount">Number of materials to take.</param>
        /// <returns>The list of materials.</returns>
        public async Task<List<Material>> GetMaterials(int startIndex, int amount)
        {
            return await _watchWordUnitOfWork.MaterialsRepository.SkipAndTakeAsync(startIndex, amount);
        }
    }
}