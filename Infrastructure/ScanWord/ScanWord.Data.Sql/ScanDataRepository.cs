using ScanWord.Core.Data;
using ScanWord.Core.Entity;

namespace ScanWord.Data.Sql
{
    /// <summary>
    /// Provides logic for working with database.
    /// </summary>
    public class ScanDataRepository : IScanDataRepository
    {
        /// <summary>
        /// Gets or sets the data base name.
        /// </summary>
        private readonly string dataBaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanDataRepository"/> class.
        /// </summary>
        /// <param name="dataBaseName">
        /// The data base name.
        /// </param>
        public ScanDataRepository(string dataBaseName)
        {
            this.dataBaseName = dataBaseName;
        }

        /// <summary>
        /// Add the file to database.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        public void AddFile(File file)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Files.Add(file);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add the word to database.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        public void AddWord(Word word)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Words.Add(word);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add the composition to database.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        public void AddComposition(Composition composition)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Compositions.Add(composition);
                db.SaveChanges();
            }
        }
    }
}