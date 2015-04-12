namespace ScanWord.Parser
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using ScanWord.Core.Common;
    using ScanWord.Core.Entity;

    using File = ScanWord.Core.Entity.File;

    /// <summary>
    /// Provides the ability to scan files and a directories for parsing words in them.
    /// </summary>
    public class ScanWordParser : IScanWordParser
    {
        /// <summary>
        /// Scans the location of words in the file.
        /// </summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <returns>Thread-safe, unordered collection of scan results.</returns>
        public ConcurrentBag<Composition> ParseFile(string absolutePath)
        {
            return this.ParseFile(absolutePath, Encoding.GetEncoding("Windows-1251"));
        }

        /// <summary>
        /// Scans the location of words in the file.
        /// </summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <param name="encoding">File content encoding.</param>
        /// <returns>Thread-safe, unordered collection of scan results.</returns>
        public ConcurrentBag<Composition> ParseFile(string absolutePath, Encoding encoding)
        {
            var lines = new Dictionary<int, string>();
            using (var stream = new StreamReader(absolutePath, encoding))
            {
                string currentLine;
                var counter = 1;
                while ((currentLine = stream.ReadLine()) != null)
                {
                    lines.Add(counter, currentLine.ToLower());
                    counter++;
                }
            }

            var scanFile = GetScanFileByPath(absolutePath);
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
        /// Get file entity by absolute path.
        /// </summary>
        /// <param name="absolutePath">
        /// Absolute path.
        /// </param>
        /// <returns>
        /// File entity. <see cref="ScanWord.Core.Entity.File"/>.
        /// </returns>
        private static File GetScanFileByPath(string absolutePath)
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
            var composition = new Composition
            {
                File = scanFile,
                Word = scanWord,
                Line = line,
                Сolumn = column
            };

            compositions.Add(composition);
        }
    }
}