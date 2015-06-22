using System;

namespace ScanWord.Core.Entity
{
    /// <summary>
    /// A pointer to a specific word in the file.
    /// </summary>
    public class Composition : IEquatable<Composition>
    {
        /// <summary>
        /// Gets or sets composition Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets link to a table of Files.
        /// </summary>
        public virtual File File { get; set; }

        /// <summary>
        /// Gets or sets the serial number of the line that contains the word.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Gets or sets link to a table of Words.
        /// </summary>
        public virtual Word Word { get; set; }

        /// <summary>
        /// Gets or sets the position of the first character in word, from the beginning of the line.
        /// </summary>
        public int Сolumn { get; set; }

        /// <summary>
        /// Equals of composition entities.
        /// </summary>
        /// <param name="other">
        /// Composition to compare.
        /// </param>
        /// <returns>
        /// Equals result. <see cref="bool"/>.
        /// </returns>
        public bool Equals(Composition other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id && File.Equals(other.File) && Сolumn == other.Сolumn
                   && Line == other.Line && Word.Equals(other.Word);
        }

        /// <summary>
        /// Equals of composition entities.
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

            return origin.GetType() == GetType() && Equals((Composition)origin);
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns>
        /// Hash code of the composition entity <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (File != null ? File.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Сolumn;
                hashCode = (hashCode * 397) ^ Line;
                hashCode = (hashCode * 397) ^ (Word != null ? Word.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}