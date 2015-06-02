using System;

namespace ScanWord.Core.Entity
{
    /// <summary>
    /// The Path, the Name and the Extension.
    /// </summary>
    public class File : IEquatable<File>
    {
        /// <summary>
        /// Gets or sets filename extension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets file name, actually.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets file Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the file, except filename and extension.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Equals of file entities.
        /// </summary>
        /// <param name="other">
        /// File to compare.
        /// </param>
        /// <returns>
        /// Equals result. <see cref="bool"/>.
        /// </returns>
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

            return this.Id == other.Id && string.Equals(this.Path, other.Path)
                   && string.Equals(this.Filename, other.Filename) && string.Equals(this.Extension, other.Extension);
        }

        /// <summary>
        /// Equals of file entities.
        /// </summary>
        /// <param name="origin">
        /// Object to compare.
        /// </param>
        /// <returns>
        /// Equals result. <see cref="bool"/>.
        /// </returns>
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

            return origin.GetType() == this.GetType() && this.Equals((File)origin);
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns>
        /// Hash code of the file entity <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Id;
                hashCode = (hashCode * 397) ^ (this.Path != null ? this.Path.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Filename != null ? this.Filename.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Extension != null ? this.Extension.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}