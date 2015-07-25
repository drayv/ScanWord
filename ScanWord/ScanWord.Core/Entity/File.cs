using System;
using ScanWord.Core.Entity.Common;

namespace ScanWord.Core.Entity
{
    /// <summary>The Path, the Name and the Extension.</summary>
    public class File : Entity<int>, IEquatable<File>
    {
        /// <summary>Gets or sets filename extension.</summary>
        public string Extension { get; set; }

        /// <summary>Gets or sets file name, actually.</summary>
        public string Filename { get; set; }

        /// <summary>Gets or sets the absolute path to the file, except filename and extension.</summary>
        public string Path { get; set; }

        /// <summary>Gets or sets the full name of the file.</summary>
        public string FullName { get; set; }

        public bool Equals(File other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Extension, other.Extension) 
                && string.Equals(Filename, other.Filename) && string.Equals(Path, other.Path) 
                && string.Equals(FullName, other.FullName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((File) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Extension != null ? Extension.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Filename != null ? Filename.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Path != null ? Path.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (FullName != null ? FullName.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(File left, File right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(File left, File right)
        {
            return !Equals(left, right);
        }
    }
}