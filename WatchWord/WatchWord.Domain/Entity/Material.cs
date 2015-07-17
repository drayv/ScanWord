using System.Collections.Concurrent;
using ScanWord.Domain.Entity;

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

    /// <summary>Material entity, film or series with information about it.</summary>
    public class Material
    {
        public Material()
        {
            Compositions = new ConcurrentBag<Composition>();
        }

        /// <summary>Gets or sets material Id.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets material type.</summary>
        public MaterialType Type { get; set; }

        /// <summary>Gets or sets ScanWord file.</summary>
        public File File { get; set; }

        /// <summary>Gets or sets ScanWord compositions list.</summary>
        public ConcurrentBag<Composition> Compositions { get; set; }

        /// <summary>Gets or sets name of material.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets description of the material.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets image of the material.</summary>
        public byte[] Image { get; set; }

        /// <summary>Gets or sets the creator of the material.</summary>
        public Account Owner { get; set; }
    }
}