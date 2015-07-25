using System;
using ScanWord.Core.Entity.Common;

namespace ScanWord.Core.Entity
{
    /// <summary>Word in the file Entity.</summary>
    public class Word : Entity<int>, IEquatable<Word>
    {
        /// <summary>Gets or sets the file.</summary>
        public File File { get; set; }

        /// <summary>Gets or sets the Good, the bad and the word.</summary>
        public string TheWord { get; set; }

        /// <summary>Gets or sets count of words in file.</summary>
        public int Count { get; set; }

        public bool Equals(Word other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Count == other.Count && Id == other.Id && Equals(File, other.File) && string.Equals(TheWord, other.TheWord);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Word) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Count;
                hashCode = (hashCode*397) ^ Id;
                hashCode = (hashCode*397) ^ (File != null ? File.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (TheWord != null ? TheWord.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Word left, Word right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Word left, Word right)
        {
            return !Equals(left, right);
        }
    }
}