﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    /// <summary>Represents a layer for work with materials.</summary>
    public interface IMaterialsService
    {
        /// <summary>Creates material with the specified attributes.</summary>
        /// <param name="subtitlesStream">Subtitles file stream.</param>
        /// <param name="imageStream">Image file stream.</param>
        /// <param name="mimeType">Mime type of image.</param>
        /// <param name="type">Material type <see cref="MaterialType"/>.</param>
        /// <param name="name">Name of the material.</param>
        /// <param name="description">Description of the material.</param>
        /// <param name="userId">Owner Id.</param>
        /// <param name="width">Material image width.</param>
        /// <param name="height">Material image height.</param>
        /// <returns>Created material by specific attributes.</returns>
        Material CreateMaterial(Stream subtitlesStream, Stream imageStream, string mimeType,
            MaterialType type, string name, string description, int userId, int width, int height);

        /// <summary>Saves material to the data storage.</summary>
        /// <param name="material">The material <see cref="Material"/>.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        Task<int> SaveMaterial(Material material);

        /// <summary>Gets the list of the materials.</summary>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>The list of the materials.</returns>
        Task<List<Material>> GetMaterialsWithFile(int currentPage, int pageSize);

        /// <summary>Gets material by Id.</summary>
        /// <param name="id">Material identity.</param>
        /// <returns>Material entity.</returns>
        Material GetMaterialWithWords(int id);

        /// <summary>Gets material by Id.</summary>
        /// <param name="id">Material identity.</param>
        /// <returns>Material entity.</returns>
        Material GetMaterial(int id);

        /// <summary>Gets total count of the materials.</summary>
        /// <returns>Total count of the materials.</returns>
        int TotalCount();

        /// <summary>Gets total count of words in the material.</summary>
        /// <param name="material">Specified material.</param>
        /// <returns>Total count of words in the material.</returns>
        int WordsCountInMaterial(Material material);
    }
}