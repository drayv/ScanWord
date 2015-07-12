using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScanWord.Core.Entity;

namespace ScanWord.Domain.Data
{
    /// <summary>
    /// Provides logic for working with database.
    /// </summary>
    public interface IScanDataRepository
    {
        /// <summary>Add the file to database.</summary>
        /// <param name="file">The file.</param>
        void AddFile(File file);

        /// <summary>Add the word to database.</summary>
        /// <param name="word">The word.</param>
        void AddWord(Word word);

        /// <summary>Add the composition to database.</summary>
        /// <param name="composition">The composition.</param>
        void AddComposition(Composition composition);

        /// <summary>Add files to database.</summary>
        /// <param name="files">Collection of files.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task<int> AddFilesAsync(ConcurrentBag<File> files);

        /// <summary>Add words to database.</summary>
        /// <param name="words">Collection of words.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task<int> AddWordsAsync(ConcurrentBag<Word> words);

        /// <summary>Add compositions to database.</summary>
        /// <param name="compositions">Collection of compositions.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task<int> AddCompositionsAsync(ConcurrentBag<Composition> compositions);

        /// <summary>Get files from database.</summary>
        /// <returns>The list of files <see cref="Task"/>.</returns>
        Task<List<File>> GetFilesAsync();

        /// <summary>The get files async.</summary>
        /// <param name="existingFiles">List of files to compare.</param>
        /// <returns>The list of files <see cref="Task"/>.</returns>
        Task<List<File>> GetFilesAsync(IQueryable<string> existingFiles);

        /// <summary>Get words from database.</summary>
        /// <returns>The list of words <see cref="Task"/>.</returns>
        Task<List<Word>> GetWordsAsync();

        /// <summary>Get words from database where TheWord in a list.</summary>
        /// <param name="existingWords">List of words to compare.</param>
        /// <returns>The list of words <see cref="Task"/>.</returns>
        Task<List<Word>> GetWordsAsync(IQueryable<string> existingWords);

        /// <summary>Get compositions from database.</summary>
        /// <returns>The list of compositions <see cref="Task"/>.</returns>
        Task<List<Composition>> GetCompositionsAsync();
    }
}