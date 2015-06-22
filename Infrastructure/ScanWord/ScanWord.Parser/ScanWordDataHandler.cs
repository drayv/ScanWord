using System.Collections.Concurrent;
using ScanWord.Core.Common;
using ScanWord.Core.Entity;

namespace ScanWord.Parser
{
    using System.Linq;

    using ScanWord.Core.Data;

    /// <summary>
    /// Provides batch work with ScanWord data.
    /// </summary>
    public class ScanWordDataHandler : IScanWordDataHandler
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        private readonly IScanDataRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanWordDataHandler"/> class.
        /// </summary>
        /// <param name="repository">ScanWord data repository.</param>
        public ScanWordDataHandler(IScanDataRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ScanWordDataHandler"/> class from being created.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private ScanWordDataHandler()
        {
        }

        /// <summary>
        /// Merge compositions with related and existing entities in database.
        /// </summary>
        /// <param name="compositions">The compositions concurrent bag.</param>
        public void MergeWithExisting(ConcurrentBag<Composition> compositions)
        {
            var materialWords = compositions.GroupBy(w => w.Word.TheWord).Select(c => c.Key).AsQueryable();
            var databaseWords = repository.GetWords(materialWords).ToDictionary(w => w.TheWord);

            foreach (var composition in compositions.Where(composition => databaseWords.ContainsKey(composition.Word.TheWord)))
            {
                composition.Word.Id = databaseWords[composition.Word.TheWord].Id;
            }
        }
    }
}