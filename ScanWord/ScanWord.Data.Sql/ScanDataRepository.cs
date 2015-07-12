﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ScanWord.Core;
using ScanWord.Core.Entity;
using ScanWord.Domain.Data;

namespace ScanWord.Data.Sql
{
    /// <summary>Provides CRUD operations for ScanWord database.</summary>
    public class ScanDataRepository : IScanDataRepository
    {
        /// <summary>Gets or sets the database name.</summary>
        private readonly string dataBaseName;

        /// <summary>Initializes a new instance of the <see cref="ScanDataRepository"/> class.</summary>
        /// <param name="settings">Project settings.</param>
        public ScanDataRepository(IProjectSettings settings)
        {
            this.dataBaseName = settings.DataBaseName;
        }

        /// <summary>Prevents a default instance of the <see cref="ScanDataRepository"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private ScanDataRepository()
        {
        }

        /// <summary>Add the file to database.</summary>
        /// <param name="file">The file.</param>
        public void AddFile(File file)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Files.Add(file);
                db.SaveChanges();
            }
        }

        /// <summary>Add the word to database.</summary>
        /// <param name="word">The word.</param>
        public void AddWord(Word word)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Words.Add(word);
                db.SaveChanges();
            }
        }

        /// <summary>Add the composition to database.</summary>
        /// <param name="composition">The composition.</param>
        public void AddComposition(Composition composition)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Compositions.Attach(composition);
                db.Entry(composition.File).State = composition.File.Id == 0 ? EntityState.Added : EntityState.Modified;
                db.Entry(composition.Word).State = composition.Word.Id == 0 ? EntityState.Added : EntityState.Modified;
                db.Entry(composition).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        /// <summary>Add files to database.</summary>
        /// <param name="files">Collection of files.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task AddFilesAsync(ConcurrentBag<File> files)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Files.AddRange(files);
                db.ChangeTracker.DetectChanges();
                await db.SaveChangesAsync();
            }
        }

        /// <summary>Add words to database.</summary>
        /// <param name="words">Collection of words.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task AddWordsAsync(ConcurrentBag<Word> words)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Words.AddRange(words);
                db.ChangeTracker.DetectChanges();
                await db.SaveChangesAsync();
            }
        }

        /// <summary>Add compositions to database.</summary>
        /// <param name="compositions">Collection of compositions.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task AddCompositionsAsync(ConcurrentBag<Composition> compositions)
        {
            ////TODO: Add delete compositions logic if file exist.
            await this.MergeWithExistingAsync(compositions);
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                foreach (var composition in compositions)
                {
                    db.Compositions.Attach(composition);
                    db.Entry(composition.File).State = composition.File.Id == default(int) ? EntityState.Added : EntityState.Unchanged;
                    db.Entry(composition.Word).State = composition.Word.Id == default(int) ? EntityState.Added : EntityState.Unchanged;
                    db.Entry(composition).State = composition.Id == default(int) ? EntityState.Added : EntityState.Unchanged;  
                }

                db.ChangeTracker.DetectChanges();
                await db.SaveChangesAsync();
            }
        }

        /// <summary>Merge compositions with related and existing entities in database.</summary>
        /// <param name="compositions">The compositions enumerable for merging.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task MergeWithExistingAsync(ConcurrentBag<Composition> compositions)
        {
            var materialFiles = compositions.GroupBy(w => w.File.FullName).Select(c => c.Key).AsQueryable();
            var databaseFiles = await this.GetFilesAsync(materialFiles);
            foreach (var composition in compositions)
            {
                var first = databaseFiles.FirstOrDefault(w => w.FullName == composition.File.FullName);
                if (first == null)
                {
                    continue;
                }

                composition.File.Id = first.Id;
                composition.FileId = first.Id;
            }

            var materialWords = compositions.GroupBy(w => w.Word.TheWord).Select(c => c.Key).AsQueryable();
            var databaseWords = await this.GetWordsAsync(materialWords);
            foreach (var composition in compositions)
            {
                var first = databaseWords.FirstOrDefault(w => w.TheWord == composition.Word.TheWord);
                if (first == null)
                {
                    continue;
                }

                composition.Word.Id = first.Id;
                composition.WordId = first.Id;
            }
        }

        /// <summary>Get files from database.</summary>
        /// <returns>The list of files <see cref="Task"/>.</returns>
        public async Task<List<File>> GetFilesAsync()
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                return await db.Files.ToListAsync();
            }
        }

        /// <summary>The get files async.</summary>
        /// <param name="existingFiles">List of files to compare.</param>
        /// <returns>The list of files <see cref="Task"/>.</returns>
        public async Task<List<File>> GetFilesAsync(IQueryable<string> existingFiles)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                return await db.Files.Where(f => existingFiles.Contains(f.FullName)).ToListAsync();
            }
        }

        /// <summary>Get words from database.</summary>
        /// <returns>The list of words <see cref="Task"/>.</returns>
        public async Task<List<Word>> GetWordsAsync()
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                return await db.Words.ToListAsync();
            }
        }

        /// <summary>Get words from database where TheWord in a list.</summary>
        /// <param name="existingWords">List of words to compare.</param>
        /// <returns>The list of words <see cref="Task"/>.</returns>
        public async Task<List<Word>> GetWordsAsync(IQueryable<string> existingWords)
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                return await db.Words.Where(w => existingWords.Contains(w.TheWord)).ToListAsync();
            }
        }

        /// <summary>Get compositions from database.</summary>
        /// <returns>The list of compositions <see cref="Task"/>.</returns>
        public async Task<List<Composition>> GetCompositionsAsync()
        {
            using (var db = new ScanDataContainer(this.dataBaseName))
            {
                return await db.Compositions.ToListAsync();
            }
        }
    }
}