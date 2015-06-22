using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ScanWord.Core;
using ScanWord.Core.Data;
using ScanWord.Core.Entity;

namespace ScanWord.Data.Sql
{
    /// <summary>
    /// Provides CRUD operations for ScanWord database.
    /// </summary>
    public class ScanDataRepository : IScanDataRepository
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        private readonly string dataBaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanDataRepository"/> class.
        /// </summary>
        /// <param name="settings">Project settings.</param>
        public ScanDataRepository(IProjectSettings settings)
        {
            this.dataBaseName = settings.DataBaseName;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ScanDataRepository"/> class from being created.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private ScanDataRepository()
        {
        }

        /// <summary>
        /// Add the file to database.
        /// </summary>
        /// <param name="file">The file.</param>
        public void AddFile(File file)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Files.Add(file);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add the word to database.
        /// </summary>
        /// <param name="word">The word.</param>
        public void AddWord(Word word)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Words.Add(word);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add the composition to database.
        /// </summary>
        /// <param name="composition">The composition.</param>
        public void AddComposition(Composition composition)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Compositions.Add(composition);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add files to database.
        /// </summary>
        /// <param name="files">Collection of files.</param>
        public void AddFiles(IEnumerable<File> files)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Files.AddRange(files);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add words to database.
        /// </summary>
        /// <param name="words">Collection of words.</param>
        public void AddWords(IEnumerable<Word> words)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Words.AddRange(words);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add compositions to database.
        /// </summary>
        /// <param name="compositions">Collection of compositions.</param>
        public void AddCompositions(IEnumerable<Composition> compositions)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                var enumerable = compositions as IList<Composition> ?? compositions.ToList();
                foreach (var composition in enumerable)
                {
                    if (composition.Word.Id != 0)
                    {
                        db.Words.Attach(composition.Word);
                    }

                    if (composition.File.Id != 0)
                    {
                        db.Files.Attach(composition.File);
                    }
                }

                /*
                var words =
                    from e in enumerable
                    where e.Word.Id != 0
                    group e by e.Word.Id into g
                    orderby g.Key
                    select g;

                foreach (var word in words)
                {
                    db.Words.Attach(word.First().Word);    
                }*/

                db.Compositions.AddRange(enumerable);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get files from database.
        /// </summary>
        /// <returns>Files concurrent bag.</returns>
        public ConcurrentBag<File> GetFiles()
        {
            ConcurrentBag<File> ret;
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                var files = db.Files.AsEnumerable();
                ret = new ConcurrentBag<File>(files);
            }

            return ret;
        }

        /// <summary>
        /// Get words from database where TheWord in a list.
        /// </summary>
        /// <param name="existingWords">List of words to compare.</param>
        /// <returns>Words concurrent bag.</returns>
        public ConcurrentBag<Word> GetWords(IQueryable<string> existingWords)
        {
            ConcurrentBag<Word> ret;
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                var words = db.Words.Where(w => existingWords.Contains(w.TheWord)).AsEnumerable();
                ret = new ConcurrentBag<Word>(words);
            }

            return ret;
        }

        /// <summary>
        /// Get words from database.
        /// </summary>
        /// <returns>Words concurrent bag.</returns>
        public ConcurrentBag<Word> GetWords()
        {
            ConcurrentBag<Word> ret;
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                var words = db.Words.AsEnumerable();
                ret = new ConcurrentBag<Word>(words);
            }

            return ret;
        }

        /// <summary>
        /// Get compositions from database.
        /// </summary>
        /// <returns>Compositions concurrent bag.</returns>
        public ConcurrentBag<Composition> GetCompositions()
        {
            ConcurrentBag<Composition> ret;
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                var compositions = db.Compositions.AsEnumerable();
                ret = new ConcurrentBag<Composition>(compositions);
            }

            return ret;
        }
    }
}