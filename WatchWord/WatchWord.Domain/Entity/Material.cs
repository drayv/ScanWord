using System.Collections.Generic;
using ScanWord.Core.Entity;
using WatchWord.Domain.Entity.Common;

namespace WatchWord.Domain.Entity
{
    /// <summary>Material type, film or series.</summary>
    public enum MaterialType
    {
        /// <summary>The film.</summary>
        Film,
        /// <summary>The series.</summary>
        Series
    }

    /// <summary>The Material entity, a film or series with information about it.</summary>
    public class Material: Entity<int>
    {
        public Material()
        {
            Words = new List<Word>();
        }

        /// <summary>Gets or sets the material type.</summary>
        public MaterialType Type { get; set; }

        /// <summary>Gets or sets the ScanWord file.</summary>
        public File File { get; set; }

        /// <summary>Gets or sets the ScanWord words list.</summary>
        public ICollection<Word> Words { get; set; }

        /// <summary>Gets or sets name of material.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the description of the material.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the image of the material.</summary>
        public byte[] Image { get; set; }

        /// <summary>Gets or sets the creator of the material.</summary>
        public Account Owner { get; set; }
    }
}