using ScanWord.Core.Entity.Common;

namespace WatchWord.Domain.Entity
{
    /// <summary>WatchWord user account entity.</summary>
    public class Account: Entity<int>
    {
        /// <summary>Gets or sets the user id.</summary>
        public int ExternalId { get; set; }
    }
}