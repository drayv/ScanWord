using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScanWord.Parser
{
    using Core.Common;
    using Core.Entity;
    using File = Core.Entity.File;

    /// <summary>
    /// Provides the ability to scan files and a directories for parsing words in them.
    /// </summary>
    public class ScanWordParser : IScanWordParser
    {
        /// <summary>
        /// Scans the location of words in the file.
        /// </summary>
        /// <param name="absolutePath">
        /// Path to the file that you want to parse.
        /// </param>
        /// <exception cref="System.IO.FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="System.NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>
        /// Thread-safe, unordered collection of scan results.
        /// </returns>
        public ConcurrentBag<Composition> ParseFile(string absolutePath)
        {
            var scanFile = GetScanFileByPath(absolutePath);
            using (var stream = OpenFileStreamReader(absolutePath))
            {
                return this.ParseFile(scanFile, stream);
            }
        }

        /// <summary>
        /// Scans the location of words in the StreamReader of the file.
        /// </summary>
        /// <param name="scanFile">
        /// Scan file entity.
        /// </param>
        /// <param name="stream">
        /// Stream reader for the text file.
        /// </param>
        /// <exception cref="System.IO.FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="System.NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>
        /// Thread-safe, unordered collection of scan results.
        /// </returns>
        public ConcurrentBag<Composition> ParseFile(File scanFile, StreamReader stream)
        {
            var lines = new Dictionary<int, string>();

            string currentLine;
            var counter = 1;
            while ((currentLine = stream.ReadLine()) != null)
            {
                lines.Add(counter, currentLine.ToLower());
                counter++;
            }

            var words = new ConcurrentBag<Composition>();

            Parallel.ForEach(
                lines,
                line =>
                {
                    var word = string.Empty;
                    var column = 1;
                    var columnCounter = 0;
                    foreach (var t in line.Value)
                    {
                        columnCounter++;
                        if (char.IsLetter(t) || t == '-' || t == '\'' || t == '’')
                        {
                            // Save position if this is a first letter of word.
                            if (word == string.Empty)
                            {
                                column = columnCounter;
                            }

                            word += t;
                        }
                        else if (word.Length > 0 && char.IsLetter(word[0]))
                        {
                            AddWordToCompositions(words, scanFile, word, line.Key, column);
                            word = string.Empty;
                        }
                        else
                        {
                            word = string.Empty;
                        }
                    }

                    AddWordToCompositions(words, scanFile, word, line.Key, column);
                });

            return words;
        }

        /// <summary>
        /// Get stream reader for the file.
        /// </summary>
        /// <param name="absolutePath">
        /// The absolute path to the file.
        /// </param>
        /// <exception cref="System.IO.FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="System.NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>
        /// The <see cref="StreamReader"/>.
        /// </returns>
        private static StreamReader OpenFileStreamReader(string absolutePath)
        {
            try
            {
                var streamReader = new StreamReader(absolutePath, true);
                return streamReader;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException(ex.Message, ex.InnerException);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Add word info to concurrent bag of compositions.
        /// </summary>
        /// <param name="compositions">
        /// The concurrent bag of compositions.
        /// </param>
        /// <param name="scanFile">
        /// File entity.
        /// </param>
        /// <param name="word">
        /// Word entity.
        /// </param>
        /// <param name="line">
        /// Serial number of the line that contains the word.
        /// </param>
        /// <param name="column">
        /// Position of the first character in word, from the beginning of the line.
        /// </param>
        private static void AddWordToCompositions(
            ConcurrentBag<Composition> compositions,
            File scanFile,
            string word,
            int line,
            int column)
        {
            if (word.Length <= 0 || !char.IsLetter(word[0]))
            {
                return;
            }

            var scanWord = GetScanWordByText(word);
            var composition = new Composition { File = scanFile, Word = scanWord, Line = line, Сolumn = column };

            compositions.Add(composition);
        }

        /// <summary>
        /// Get file entity by absolute path.
        /// </summary>
        /// <param name="absolutePath">
        /// Absolute path.
        /// </param>
        /// <returns>
        /// <exception cref="System.NotSupportedException">If file from absolute path don't support read or a security error is detected.</exception>
        /// File entity. <see cref="ScanWord.Core.Entity.File"/>.
        /// </returns>
        private static File GetScanFileByPath(string absolutePath)
        {
            try
            {
                var fileInfo = new FileInfo(absolutePath);
                var scanFile = new File
                {
                    Filename = fileInfo.Name,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.DirectoryName
                };
                return scanFile;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Get word entity by text.
        /// </summary>
        /// <param name="wordText">
        /// The word text.
        /// </param>
        /// <returns>
        /// Word entity. <see cref="ScanWord.Core.Entity.Word"/>.
        /// </returns>
        private static Word GetScanWordByText(string wordText)
        {
            var scanWord = new Word { TheWord = wordText };
            return scanWord;
        }
    }
}