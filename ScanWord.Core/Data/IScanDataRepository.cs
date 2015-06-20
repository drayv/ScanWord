using ScanWord.Core.Entity;

namespace ScanWord.Core.Data
{
    /// <summary>
    /// Provides logic for working with database.
    /// </summary>
    public interface IScanDataRepository
    {
        /// <summary>
        /// Add the file to database.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        void AddFile(File file);

        /// <summary>
        /// Add the word to database.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        void AddWord(Word word);

        /// <summary>
        /// Add the composition to database.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        void AddComposition(Composition composition);
    }
}