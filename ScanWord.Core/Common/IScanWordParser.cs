using System.Collections.Concurrent;
using System.IO;
using ScanWord.Core.Entity;
using File = ScanWord.Core.Entity.File;

namespace ScanWord.Core.Common
{
    /// <summary>
    /// Provides the ability to scan files and a directories for parsing words in them.
    /// </summary>
    public interface IScanWordParser
    {
        /// <summary>
        /// Scans the location of words in the file.
        /// </summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <param name="existingWords">Existing words to be used in the composition.</param>
        /// <exception cref="System.IO.FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="System.NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Thread-safe, unordered collection of scan results.</returns>
        ConcurrentBag<Composition> ParseFile(string absolutePath, ConcurrentBag<Word> existingWords);

        /// <summary>
        /// Scans the location of words in the StreamReader of the file.
        /// </summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="existingWords">Existing words to be used in the composition.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <exception cref="System.IO.FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="System.NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Thread-safe, unordered collection of scan results.</returns>
        ConcurrentBag<Composition> ParseFile(File scanFile, ConcurrentBag<Word> existingWords, StreamReader stream);
    }
}