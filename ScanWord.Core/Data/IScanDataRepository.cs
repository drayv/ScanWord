using System.Collections.Concurrent;
using System.Linq;
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
        /// <param name="file">The file.</param>
        void AddFile(File file);

        /// <summary>
        /// Add the word to database.
        /// </summary>
        /// <param name="word">The word.</param>
        void AddWord(Word word);

        /// <summary>
        /// Add the composition to database.
        /// </summary>
        /// <param name="composition">The composition.</param>
        void AddComposition(Composition composition);

        /// <summary>
        /// Add files to database.
        /// </summary>
        /// <param name="files">Collection of files.</param>
        void AddFiles(ConcurrentBag<File> files);

        /// <summary>
        /// Add words to database.
        /// </summary>
        /// <param name="words">Collection of words.</param>
        void AddWords(ConcurrentBag<Word> words);

        /// <summary>
        /// Add compositions to database.
        /// </summary>
        /// <param name="compositions">Collection of compositions.</param>
        void AddCompositions(ConcurrentBag<Composition> compositions);

        /// <summary>
        /// Get files from database.
        /// </summary>
        /// <returns>Files concurrent bag.</returns>
        ConcurrentBag<File> GetFiles();

        /// <summary>
        /// Get words from database where TheWord in a list.
        /// </summary>
        /// <param name="existingWords">List of words to compare.</param>
        /// <returns>Words concurrent bag.</returns>
        ConcurrentBag<Word> GetWords(IQueryable<string> existingWords);

        /// <summary>
        /// Get words from database.
        /// </summary>
        /// <returns>Words concurrent bag.</returns>
        ConcurrentBag<Word> GetWords();

        /// <summary>
        /// Get compositions from database.
        /// </summary>
        /// <returns>Compositions concurrent bag.</returns>
        ConcurrentBag<Composition> GetCompositions();
    }
}