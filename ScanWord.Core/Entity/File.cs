using System;
using System.Collections.Generic;

namespace ScanWord.Core.Entity
{
    /// <summary>
    /// The Path, the Name and the Extension.
    /// </summary>
    public class File : IEquatable<File>
    {
        /// <summary>
        /// The compositions link.
        /// </summary>
        private ICollection<Composition> compositions;

        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        public File()
        {
            this.compositions = new HashSet<Composition>();
        }

        /// <summary>
        /// Gets or sets file Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets filename extension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets file name, actually.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the file, except filename and extension.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the full name of the file.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the compositions.
        /// </summary>
        public virtual ICollection<Composition> Compositions
        {
            get
            {
                return this.compositions;
            }

            set
            {
                this.compositions = value;
            }
        }

        /// <summary>
        /// Equals of file entities.
        /// </summary>
        /// <param name="other">File to compare.</param>
        /// <returns>Equals result <see cref="bool"/>.</returns>
        public bool Equals(File other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id && string.Equals(Path, other.Path) && string.Equals(Filename, other.Filename)
                && string.Equals(Extension, other.Extension) && string.Equals(FullName, other.FullName) && Compositions.Equals(other.Compositions);
        }

        /// <summary>
        /// Equals of file entities.
        /// </summary>
        /// <param name="origin">Object to compare.</param>
        /// <returns>Equals result <see cref="bool"/>.</returns>
        public override bool Equals(object origin)
        {
            if (ReferenceEquals(null, origin))
            {
                return false;
            }

            if (ReferenceEquals(this, origin))
            {
                return true;
            }

            return origin.GetType() == GetType() && Equals((File)origin);
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns>Hash code of the file entity <see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Path != null ? Path.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Filename != null ? Filename.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Extension != null ? Extension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Compositions != null ? Compositions.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}