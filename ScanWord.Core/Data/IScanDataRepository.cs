using System.Collections.Concurrent;
using System.Collections.Generic;
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
        void AddFiles(IEnumerable<File> files);

        /// <summary>
        /// Add words to database.
        /// </summary>
        /// <param name="words">Collection of word.</param>
        void AddWords(IEnumerable<Word> words);

        /// <summary>
        /// Add compositions to database.
        /// </summary>
        /// <param name="compositions">Collection of composition.</param>
        void AddCompositions(IEnumerable<Composition> compositions);

        /// <summary>
        /// Get files from database.
        /// </summary>
        /// <returns>Files concurrent bag.</returns>
        ConcurrentBag<File> GetFiles();

        /// <summary>
        /// Get files from database.
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