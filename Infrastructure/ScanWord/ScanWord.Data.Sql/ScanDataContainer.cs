using System.Data.Entity;
using ScanWord.Core.Entity;

namespace ScanWord.Data.Sql
{
    /// <summary>
    /// Represents a Unit Of Work with ScanWord database.
    /// </summary>
    public class ScanDataContainer : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanDataContainer"/> class.
        /// </summary>
        /// <param name="dataBaseName">
        /// The database name.
        /// </param>
        public ScanDataContainer(string dataBaseName)
            : base("name=" + dataBaseName)
        {
        }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        public DbSet<File> Files { get; set; }

        /// <summary>
        /// Gets or sets the words.
        /// </summary>
        public DbSet<Word> Words { get; set; }

        /// <summary>
        /// Gets or sets the compositions.
        /// </summary>
        public DbSet<Composition> Compositions { get; set; }
    }
}