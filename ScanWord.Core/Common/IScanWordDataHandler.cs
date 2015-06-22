using System.Collections.Concurrent;
using ScanWord.Core.Entity;

namespace ScanWord.Core.Common
{
    /// <summary>
    /// Provides batch work with ScanWord data.
    /// </summary>
    public interface IScanWordDataHandler
    {
        /// <summary>
        /// Merge compositions with related and existing entities in database.
        /// </summary>
        /// <param name="compositions">The compositions concurrent bag.</param>
        void MergeWithExisting(ConcurrentBag<Composition> compositions);
    }
}