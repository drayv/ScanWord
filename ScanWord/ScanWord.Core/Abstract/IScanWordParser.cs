using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScanWord.Core.Entity;
using File = ScanWord.Core.Entity.File;

namespace ScanWord.Core.Abstract
{
    /// <summary>Provides the ability to parsing words in the files or streams.</summary>
    public interface IScanWordParser
    {
        /// <summary>Scans the unique words in the file with default Windows-1251 character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of words in file.</returns>
        List<Word> ParseUnigueWordsInFile(string absolutePath);

        /// <summary>Scans all the words and their positions in the file with default Windows-1251 character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of word compositions in file.</returns>
        List<Composition> ParseAllWordsInFile(string absolutePath);

        /// <summary>Scans the unique words in the file with custom character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <param name="encoding">Character encoding.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of words in file.</returns>
        List<Word> ParseUnigueWordsInFile(string absolutePath, Encoding encoding);

        /// <summary>Scans all the words and their positions in the file with custom character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <param name="encoding">Character encoding.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of word compositions in file.</returns>
        List<Composition> ParseAllWordsInFile(string absolutePath, Encoding encoding);

        /// <summary>Scans the unique words in the StreamReader of the file.</summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of words in file.</returns>
        List<Word> ParseUnigueWordsInFile(File scanFile, StreamReader stream);

        /// <summary>Scans all the words and their positions in the StreamReader of the file.</summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of word compositions in file.</returns>
        List<Composition> ParseAllWordsInFile(File scanFile, StreamReader stream);
    }
}