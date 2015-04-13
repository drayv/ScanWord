// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScanWordParser.cs" company="Maksym Shchyhol">
//   Copyright (c) Maksym Shchyhol. All Rights Reserved
// </copyright>
// <summary>
//   Provides the ability to scan files and a directories for parsing words in them.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScanWord.Core.Common
{
    using System.Collections.Concurrent;
    using System.Text;

    using ScanWord.Core.Entity;

    /// <summary>
    /// Provides the ability to scan files and a directories for parsing words in them.
    /// </summary>
    public interface IScanWordParser
    {
        /// <summary>
        /// Scans the location of words in the file.
        /// </summary>
        /// <param name="absolutePath">
        /// Path to the file that you want to parse.
        /// </param>
        /// <returns>
        /// Thread-safe, unordered collection of scan results.
        /// </returns>
        ConcurrentBag<Composition> ParseFile(string absolutePath);

        /// <summary>
        /// Scans the location of words in the file.
        /// </summary>
        /// <param name="absolutePath">
        /// Path to the file that you want to parse.
        /// </param>
        /// <param name="encoding">
        /// File content encoding.
        /// </param>
        /// <returns>
        /// Thread-safe, unordered collection of scan results.
        /// </returns>
        ConcurrentBag<Composition> ParseFile(string absolutePath, Encoding encoding);
    }
}