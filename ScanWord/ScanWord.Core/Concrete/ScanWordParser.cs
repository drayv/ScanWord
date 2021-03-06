﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ScanWord.Core.Abstract;
using ScanWord.Core.Entity;
using File = ScanWord.Core.Entity.File;

namespace ScanWord.Core.Concrete
{
    /// <summary>Represents logic for parsing words in the files or streams.</summary>
    public class ScanWordParser : IScanWordParser
    {
        /// <summary>Scans the unique words in the file with default Windows-1251 character encoding.</summary>
        /// <param name="absolutePath">Path to the file that you want to parse.</param>
        /// <exception cref="FileNotFoundException">Specified pathname does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        /// <returns>Unsorted collection of words in file.</returns>
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
        /// <exception cref="FileNotFoundException">Specified pathname does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        /// <returns>Unsorted collection of word compositions in file.</returns>
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
        /// <exception cref="FileNotFoundException">Specified pathname does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        /// <returns>Unsorted collection of words in file.</returns>
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
        /// <exception cref="FileNotFoundException">Specified pathname does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        /// <returns>Unsorted collection of word compositions in file.</returns>
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
        /// <exception cref="FileNotFoundException">Specified pathname does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        /// <returns>Unsorted collection of words in file.</returns>
        public List<Word> ParseUnigueWordsInFile(File scanFile, StreamReader stream)
        {
            return ParseFile(scanFile, stream, TypeResult.OnlyUniqueWordsInFile).Words;
        }

        /// <summary>Scans all the words and their positions in the StreamReader of the file.</summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <exception cref="FileNotFoundException">Specified pathname does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        /// <returns>Unsorted collection of word compositions in file.</returns>
        public List<Composition> ParseAllWordsInFile(File scanFile, StreamReader stream)
        {
            return ParseFile(scanFile, stream, TypeResult.CompositionOfWords).Compositions;
        }

        /// <summary>Scans the location of words in the StreamReader of the file.</summary>
        /// <param name="scanFile">Scan file entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <param name="type">Type of result.</param>
        /// <exception cref="FileNotFoundException">Specified pathname does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        private static ScanResult ParseFile(File scanFile, TextReader stream, TypeResult type)
        {
            var pattern = new Regex(@"[^\W_\d]([^\W_\d]|[-’'](?=[^\W_\d]))*([^\W_\d]|['’])?");

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
                    var words = pattern.Matches(line.Value);
                    for (var i = 0; i < words.Count; i++)
                    {
                        var scanWord = GetOrCreateScanWord(wordsLocker, fileWords, scanFile, words[i].Value);
                        if (type == TypeResult.CompositionOfWords)
                        {
                            AddWordToCompositions(compositionsLocker, compositions, scanWord, line.Key, words[i].Index + 1);
                        }
                    }
                });

            return new ScanResult { Words = fileWords, Compositions = compositions };
        }

        /// <summary>Gets a stream reader using the absolute path of the file.</summary>
        /// <param name="absolutePath">The absolute path to the file.</param>
        /// <param name="encoding">Character encoding.</param>
        /// <exception cref="FileNotFoundException">Specified pathname does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        /// <returns>The <see cref="StreamReader"/> of the file.</returns>
        private static StreamReader OpenFileStreamReader(string absolutePath, Encoding encoding)
        {
            try
            {
                var streamReader = new StreamReader(absolutePath, encoding);
                return streamReader;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new System.NotSupportedException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>Adds word info to collection of compositions.</summary>
        /// <param name="compositionsLocker">Mutex for adding compositions.</param>
        /// <param name="compositions">The collection of word compositions.</param>
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

        /// <summary>Gets file entity using absolute path.</summary>
        /// <param name="absolutePath">Absolute path.</param>
        /// <exception cref="System.NotSupportedException">Specified file is not supports read or security error is detected.</exception>
        /// <returns>The <see cref="File"/> entity.</returns>
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
            catch (System.Exception ex)
            {
                throw new System.NotSupportedException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>Gets or creates word entity using the word string.</summary>
        /// <param name="wordsLocker">Mutex for adding words.</param>
        /// <param name="fileWords">Existing words to compare.</param>
        /// <param name="scanFile">File containing this word.</param>
        /// <param name="wordText">The word string.</param>
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

                word = new Word { File = scanFile, TheWord = wordText, Count = 1 };
                fileWords.Add(word);
            }

            return word;
        }

        /// <summary>Nested type of scan result.</summary>
        private class ScanResult
        {
            /// <summary>Gets or sets unsorted collection of words in the file.</summary>
            public List<Word> Words { get; set; }

            /// <summary>Gets or sets unsorted collection of word compositions in the file.</summary>
            public List<Composition> Compositions { get; set; }
        }
    }
}