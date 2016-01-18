using System;
using ScanWord.Core.Entity.Common;

namespace ScanWord.Core.Entity
{
    /// <summary>A pointer to a specific word in the file.</summary>
    public class Composition : Entity<int>, IEquatable<Composition>
    {
        /// <summary>Gets or sets link to the words table.</summary>
        public virtual Word Word { get; set; }

        /// <summary>Gets or sets the serial number of the line that contains the word.</summary>
        public int Line { get; set; }

        /// <summary>Gets or sets the position of the first character in word from the beginning of the line.</summary>
        public int Сolumn { get; set; }

        public bool Equals(Composition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Equals(Word, other.Word) && Line == other.Line && Сolumn == other.Сolumn;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Composition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Word != null ? Word.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Line;
                hashCode = (hashCode*397) ^ Сolumn;
                return hashCode;
            }
        }

        public static bool operator ==(Composition left, Composition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Composition left, Composition right)
        {
            return !Equals(left, right);
        }
    }
}