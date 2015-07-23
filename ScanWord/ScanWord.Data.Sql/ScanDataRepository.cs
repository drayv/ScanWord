using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using ScanWord.Core.Data;
using ScanWord.Core.Entity;
using ScanWord.Domain;

namespace ScanWord.Data.Sql
{
    /// <summary>Provides CRUD operations for ScanWord database.</summary>
    public class ScanDataRepository : IScanDataRepository
    {
        /// <summary>Gets or sets the database name.</summary>
        private readonly string _dataBaseName;

        /// <summary>Initializes a new instance of the <see cref="ScanDataRepository"/> class.</summary>
        /// <param name="settings">Project settings.</param>
        public ScanDataRepository(IScanProjectSettings settings)
        {
            _dataBaseName = settings.DataBaseName;
        }

        /// <summary>Prevents a default instance of the <see cref="ScanDataRepository"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private ScanDataRepository()
        {
        }

        #region CREATE

        /// <summary>Add the file to database.</summary>
        /// <param name="file">The file.</param>
        public void AddFile(File file)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                db.Files.Add(file);
                db.SaveChanges();
            }
        }

        /// <summary>Add the word to database.</summary>
        /// <param name="word">The word.</param>
        public void AddWord(Word word)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                db.Entry(word).State = EntityState.Added;
                db.Entry(word.File).State = word.File.Id > 0 ? EntityState.Unchanged : EntityState.Added;
                db.SaveChanges();
            }
        }

        /// <summary>Add the composition to database.</summary>
        /// <param name="composition">The composition.</param>
        public void AddComposition(Composition composition)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                db.Entry(composition).State = EntityState.Added;
                db.Entry(composition.Word).State = composition.Word.Id > 0 ? EntityState.Unchanged : EntityState.Added;
                db.SaveChanges();
            }
        }

        /// <summary>Add files to database.</summary>
        /// <param name="files">Collection of files.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<int> AddFilesAsync(IEnumerable<File> files)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Files.AddRange(files);
                db.ChangeTracker.DetectChanges();
                return await db.SaveChangesAsync();
            }
        }

        /// <summary>Add words to database.</summary>
        /// <param name="words">Collection of words.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<int> AddWordsAsync(IEnumerable<Word> words)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                foreach (var word in words)
                {
                    db.Entry(word).State = EntityState.Added;
                    db.Entry(word.File).State = word.File.Id > 0 ? EntityState.Unchanged : EntityState.Added;
                }

                db.ChangeTracker.DetectChanges();
                return await db.SaveChangesAsync();
            }
        }

        /// <summary>Add compositions to database.</summary>
        /// <param name="compositions">Collection of compositions.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<int> AddCompositionsAsync(IEnumerable<Composition> compositions)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                foreach (var composition in compositions)
                {
                    db.Entry(composition).State = EntityState.Added;
                    db.Entry(composition.Word).State = composition.Word.Id > 0 ? EntityState.Unchanged : EntityState.Added;
                }

                db.ChangeTracker.DetectChanges();
                return await db.SaveChangesAsync();
            }
        }

        #endregion

        #region READ

        /// <summary>Get files from database.</summary>
        /// <returns>The list of files <see cref="Task"/>.</returns>
        public async Task<List<File>> GetFilesAsync()
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                return await db.Files.ToListAsync();
            }
        }

        /// <summary>The get files async.</summary>
        /// <param name="existingFiles">List of files to compare.</param>
        /// <returns>The list of files <see cref="Task"/>.</returns>
        public async Task<List<File>> GetFilesAsync(IQueryable<string> existingFiles)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                return await db.Files.Where(f => existingFiles.Contains(f.FullName)).ToListAsync();
            }
        }

        /// <summary>Get words from database.</summary>
        /// <returns>The list of words <see cref="Task"/>.</returns>
        public async Task<List<Word>> GetWordsAsync()
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                return await db.Words.ToListAsync();
            }
        }

        /// <summary>Get words from database where TheWord in a list.</summary>
        /// <param name="existingWords">List of words to compare.</param>
        /// <returns>The list of words <see cref="Task"/>.</returns>
        public async Task<List<Word>> GetWordsAsync(IQueryable<string> existingWords)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                return await db.Words.Where(w => existingWords.Contains(w.TheWord)).ToListAsync();
            }
        }

        /// <summary>Get compositions from database.</summary>
        /// <returns>The list of compositions <see cref="Task"/>.</returns>
        public async Task<List<Composition>> GetCompositionsAsync()
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                return await db.Compositions.ToListAsync();
            }
        }

        #endregion

        #region UPDATE
        #endregion

        #region DELETE

        public async Task<int> DeleteCompositionsByFileIdAsync(IQueryable<int> fileIdList)
        {
            using (var db = new ScanDataContainer(_dataBaseName))
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Compositions.Where(c => fileIdList.Contains(c.Word.File.Id)).Delete();
                db.ChangeTracker.DetectChanges();
                return await db.SaveChangesAsync();
            }
        }

        #endregion
    }
}