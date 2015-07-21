using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanWord.Core.Abstract;
using ScanWord.Core.Entity;
using File = ScanWord.Core.Entity.File;

namespace ScanWord.Core.Concrete
{
    /// <summary>Provides the ability to parsing words in the files or streams.</summary>
    public class ScanWordParser : IScanWordParser
    {
        /// <summary>Scans the unique words in the file with default Windows-1251 character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of words in file.</returns>
        public List<Word> ParseUnigueWordsInFile(string absolutePath)
        {
            var scanFile = GetScanFileByPath(absolutePath);
            using (var stream = OpenFileStreamReader(absolutePath, Encoding.GetEncoding("Windows-1251")))
            {
                return ParseUnigueWordsInFile(scanFile, stream);
            }
        }

        /// <summary>Scans all the words and their positions in the file with default Windows-1251 character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of word compositions in file.</returns>
        public List<Composition> ParseAllWordsInFile(string absolutePath)
        {
            var scanFile = GetScanFileByPath(absolutePath);
            using (var stream = OpenFileStreamReader(absolutePath, Encoding.GetEncoding("Windows-1251")))
            {
                return ParseAllWordsInFile(scanFile, stream);
            }
        }

        /// <summary>Scans the unique words in the file with custom character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <param name="encoding">Character encoding.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of words in file.</returns>
        public List<Word> ParseUnigueWordsInFile(string absolutePath, Encoding encoding)
        {
            var scanFile = GetScanFileByPath(absolutePath);
            using (var stream = OpenFileStreamReader(absolutePath, encoding))
            {
                return ParseUnigueWordsInFile(scanFile, stream);
            }
        }

        /// <summary>Scans all the words and their positions in the file with custom character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <param name="encoding">Character encoding.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of word compositions in file.</returns>
        public List<Composition> ParseAllWordsInFile(string absolutePath, Encoding encoding)
        {
            var scanFile = GetScanFileByPath(absolutePath);
            using (var stream = OpenFileStreamReader(absolutePath, encoding))
            {
                return ParseAllWordsInFile(scanFile, stream);
            }
        }

        /// <summary>Scans the unique words in the StreamReader of the file.</summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of words in file.</returns>
        public List<Word> ParseUnigueWordsInFile(File scanFile, StreamReader stream)
        {
            return ParseFile(scanFile, stream, TypeResult.OnlyUniqueWordsInFile).Words;
        }

        /// <summary>Scans all the words and their positions in the StreamReader of the file.</summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>Unordered collection of word compositions in file.</returns>
        public List<Composition> ParseAllWordsInFile(File scanFile, StreamReader stream)
        {
            return ParseFile(scanFile, stream, TypeResult.CompositionOfWords).Compositions;
        }

        /// <summary>Scans the location of words in the StreamReader of the file.</summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <param name="type">Type of result.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        private static ScanResult ParseFile(File scanFile, TextReader stream, TypeResult type)
        {
            var wordsLocker = new object();
            var compositionsLocker = new object();

            var fileWords = new List<Word>();
            var compositions = new List<Composition>();
            var lines = new Dictionary<int, string>();

            string currentLine;
            var counter = 1;
            while ((currentLine = stream.ReadLine()) != null)
            {
                lines.Add(counter, currentLine.ToLower());
                counter++;
            }

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
                            var scanWord = GetOrCreateScanWord(wordsLocker, fileWords, scanFile, word);

                            if (type == TypeResult.CompositionOfWords)
                            {
                                AddWordToCompositions(compositionsLocker, compositions, scanWord, line.Key, column);
                            }

                            word = string.Empty;
                        }
                        else
                        {
                            word = string.Empty;
                        }
                    }

                    if (word.Length <= 0 || !char.IsLetter(word[0])) return;
                    var lastWord = GetOrCreateScanWord(wordsLocker, fileWords, scanFile, word);

                    if (type == TypeResult.CompositionOfWords)
                    {
                        AddWordToCompositions(compositionsLocker, compositions, lastWord, line.Key, column);
                    }
                });

            return new ScanResult { Words = fileWords, Compositions = compositions };
        }

        /// <summary>Get stream reader for the file.</summary>
        /// <param name="absolutePath">The absolute path to the file.</param>
        /// <param name="encoding">Character encoding.</param>
        /// <exception cref="FileNotFoundException">Absolute path lead to the not existing file.</exception>
        /// <exception cref="DirectoryNotFoundException">Absolute path lead to the not existing directory.</exception>
        /// <exception cref="NotSupportedException">File from absolute path don't support read or a security error is detected.</exception>
        /// <returns>The <see cref="StreamReader"/> of the file.</returns>
        private static StreamReader OpenFileStreamReader(string absolutePath, Encoding encoding)
        {
            try
            {
                var streamReader = new StreamReader(absolutePath, encoding);
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

        /// <summary>Add word info to concurrent bag of compositions.</summary>
        /// <param name="compositionsLocker">Mutex for adding compositions.</param>
        /// <param name="compositions">The concurrent bag of word compositions.</param>
        /// <param name="scanWord">Word entity.</param>
        /// <param name="line">Serial number of the line that contains the word.</param>
        /// <param name="column">Position of the first character in word, from the beginning of the line.</param>
        private static void AddWordToCompositions(
            object compositionsLocker,
            ICollection<Composition> compositions,
            Word scanWord,
            int line,
            int column)
        {

            var composition = new Composition { Word = scanWord, Line = line, Сolumn = column };

            lock (compositionsLocker)
            {
                compositions.Add(composition);
            }
        }

        /// <summary>Get file entity by absolute path.</summary>
        /// <param name="absolutePath">Absolute path.</param>
        /// <returns>
        /// <exception cref="NotSupportedException">If file from absolute path don't support read or a security error is detected.</exception>
        /// The <see cref="File"/> entity.
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
                    Path = fileInfo.DirectoryName,
                    FullName = fileInfo.FullName
                };
                return scanFile;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>Get or create word entity by text.</summary>
        /// <param name="wordsLocker">Mutex for adding words.</param>
        /// <param name="fileWords">Existing words to compare.</param>
        /// <param name="scanFile">File containing this word.</param>
        /// <param name="wordText">The word text.</param>
        /// <returns>The <see cref="Word"/> entity.</returns>
        private static Word GetOrCreateScanWord(object wordsLocker, ICollection<Word> fileWords, File scanFile, string wordText)
        {
            Word word;
            lock (wordsLocker)
            {
                word = fileWords.FirstOrDefault(w => w.TheWord == wordText);

                if (!Equals(word, default(Word)))
                {
                    word.Count++;
                    return word;
                }

                word = new Word { File = scanFile, TheWord = wordText, Count = 1};
                fileWords.Add(word);
            }

            return word;
        }

        /// <summary>Nested type for scan result.</summary>
        private class ScanResult
        {
            /// <summary>Gets or sets Unordered collection of words in file.</summary>
            public List<Word> Words { get; set; }

            /// <summary>Gets or sets Unordered collection of word compositions in file.</summary>
            public List<Composition> Compositions { get; set; }
        }
    }
}