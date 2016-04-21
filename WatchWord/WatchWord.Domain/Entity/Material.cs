﻿using ScanWord.Core.Entity;
using ScanWord.Core.Entity.Common;

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
    public class Material : Entity<int>
    {
        /// <summary>Gets or sets the material type.</summary>
        public MaterialType Type { get; set; }

        /// <summary>Gets or sets the ScanWord file.</summary>
        public File File { get; set; }

        /// <summary>Gets or sets name of the material.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the description of the material.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the image of the material.</summary>
        public byte[] Image { get; set; }

        /// <summary>Gets or sets mime type of material image.</summary>
        public string MimeType { get; set; }

        /// <summary>Gets or sets the creator of the material.</summary>
        public Account Owner { get; set; }
    }
}