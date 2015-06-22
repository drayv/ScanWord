using System;

namespace ScanWord.Core.Entity
{
    /// <summary>
    /// Word Entity.
    /// </summary>
    public class Word : IEquatable<Word>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Good, the bad and the word.
        /// </summary>
        public string TheWord { get; set; }

        /// <summary>
        /// Equals of word entities.
        /// </summary>
        /// <param name="other">
        /// Word to compare.
        /// </param>
        /// <returns>
        /// Equals result. <see cref="bool"/>.
        /// </returns>
        public bool Equals(Word other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id && string.Equals(TheWord, other.TheWord);
        }

        /// <summary>
        /// Equals of word entities.
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

            return origin.GetType() == GetType() && Equals((Word)origin);
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns>
        /// Hash code of the word entity <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (TheWord != null ? TheWord.GetHashCode() : 0);
            }
        }
    }
}