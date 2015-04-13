// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Word.cs" company="Maksym Shchyhol">
//   Copyright (c) Maksym Shchyhol. All Rights Reserved
// </copyright>
// <summary>
//   Word Entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScanWord.Core.Entity
{
    using System;

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

            return this.Id == other.Id && string.Equals(this.TheWord, other.TheWord);
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

            return origin.GetType() == this.GetType() && this.Equals((Word)origin);
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
                return (this.Id * 397) ^ (this.TheWord != null ? this.TheWord.GetHashCode() : 0);
            }
        }
    }
}