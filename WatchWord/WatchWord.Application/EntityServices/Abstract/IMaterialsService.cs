using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    /// <summary>Represents a layer for work with materials.</summary>
    public interface IMaterialsService
    {
        /// <summary>Creates material with the specified attributes.</summary>
        /// <param name="stream">Subtitles file stream.</param>
        /// <param name="type">Material type <see cref="MaterialType"/>.</param>
        /// <param name="name">Name of the material.</param>
        /// <param name="description">Description of the material.</param>
        /// <param name="userId">Owner Id.</param>
        /// <returns>Created material by specific attributes.</returns>
        Material CreateMaterial(Stream stream, MaterialType type, string name, string description, int userId);

        /// <summary>Saves material to the data storage.</summary>
        /// <param name="material">The material <see cref="Material"/>.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        Task<int> SaveMaterial(Material material);

        /// <summary>Skip the given number and returns the specified number of materials from the data storage.</summary>
        /// <param name="startIndex">>Number of materials to skip.</param>
        /// <param name="amount">Number of materials to take.</param>
        /// <returns>The list of materials.</returns>
        Task<List<Material>> GetMaterials(int startIndex, int amount);
    }
}