using System.Collections.Concurrent;
using System.IO;
using System.Text;
using ScanWord.Domain.Entity;
using File = ScanWord.Domain.Entity.File;

namespace ScanWord.Domain.Common
{
    /// <summary>
    /// Provides the ability to scan files and a directories for parsing words in them.
    /// </summary>
    public interface IScanWordParser
    {
        /// <summary>Scans the location of words in the file with default Windows-1251 character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <exception cref="System.IO.FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="System.NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Thread-safe, unordered collection of scan results.</returns>
        ConcurrentBag<Composition> ParseFile(string absolutePath);

        /// <summary>Scans the location of words in the file with custom character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <param name="encoding">Character encoding.</param>
        /// <exception cref="System.IO.FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="System.NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Thread-safe, unordered collection of scan results.</returns>
        ConcurrentBag<Composition> ParseFile(string absolutePath, Encoding encoding);

        /// <summary>Scans the location of words in the StreamReader of the file.</summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <exception cref="System.IO.FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="System.NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Thread-safe, unordered collection of scan results.</returns>
        ConcurrentBag<Composition> ParseFile(File scanFile, StreamReader stream);
    }
}