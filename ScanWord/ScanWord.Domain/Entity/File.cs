using System;

namespace ScanWord.Domain.Entity
{
    /// <summary>
    /// The Path, the Name and the Extension.
    /// </summary>
    public class File : IEquatable<File>
    {
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
                && string.Equals(Extension, other.Extension) && string.Equals(FullName, other.FullName);
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
                return hashCode;
            }
        }
    }
}