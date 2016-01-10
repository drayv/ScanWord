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
        private readonly IImageService _imageService;
        private readonly IAccountService _accountService;

        /// <summary>Prevents a default instance of the <see cref="MaterialsService"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private MaterialsService()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MaterialsService"/> class.</summary>
        /// <param name="watchWordUnitOfWork">Unit of work over WatchWord repositories.</param>
        /// <param name="parser">Words parser.</param>
        /// <param name="imageService">Image service.</param>
        /// <param name="accountService">Account service.</param>
        public MaterialsService(IWatchWordUnitOfWork watchWordUnitOfWork, IScanWordParser parser, IImageService imageService, IAccountService accountService)
        {
            _watchWordUnitOfWork = watchWordUnitOfWork;
            _imageService = imageService;
            _accountService = accountService;
            _parser = parser;
        }

        /// <summary>Creates material with the specified attributes.</summary>
        /// <param name="subtitlesStream">Subtitles file stream.</param>
        /// <param name="imageStream">Image file stream.</param>
        /// <param name="type">Material type <see cref="MaterialType"/>.</param>
        /// <param name="name">Name of the material.</param>
        /// <param name="description">Description of the material.</param>
        /// <param name="userId">Owner Id.</param>
        /// <param name="width">Material image width.</param>
        /// <param name="height">Material image height.</param>
        /// <returns>Created material by specific attributes.</returns>
        public Material CreateMaterial(Stream subtitlesStream, Stream imageStream, MaterialType type, string name,
            string description, int userId, int width, int height)
        {
            //TODO: this material exist check
            var material = new Material { Description = description, Name = name, Type = type };

            using (var streamReader = new StreamReader(subtitlesStream, Encoding.GetEncoding("Windows-1251")))
            {
                var file = new File { Path = userId.ToString(), Filename = name, Extension = type.ToString(), FullName = userId + name + type };
                file.Words = _parser.ParseUnigueWordsInFile(file, streamReader);
                material.File = file;
            }

            material.Image = _imageService.CropImage(imageStream, width, height);
            material.Owner = _accountService.GetByExternalId(userId);

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

        /// <summary>Take the list of materials.</summary>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>The list of materials.</returns>
        public async Task<List<Material>> GetMaterials(int currentPage, int pageSize)
        {
            var pagesToSkip = (currentPage <= 0 ? 1 : currentPage) - 1;
            return await _watchWordUnitOfWork.MaterialsRepository.SkipAndTakeAsync(pagesToSkip * pageSize, pageSize, null, n => n.File.Words);
        }

        /// <summary>Gets material by Id.</summary>
        /// <param name="id">Material identity.</param>
        /// <returns>Material entity.</returns>
        public Material GetMaterial(int id)
        {
            return _watchWordUnitOfWork.MaterialsRepository.GetByСondition(m => m.Id == id, m => m.File.Words);
        }

        /// <summary>Gets the total count of materils.</summary>
        /// <returns>Total count of materials.</returns>
        public int TotalCount()
        {
            return _watchWordUnitOfWork.MaterialsRepository.GetCount();
        }
    }
}