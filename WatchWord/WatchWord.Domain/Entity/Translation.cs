using System;
using ScanWord.Core.Entity.Common;

namespace WatchWord.Domain.Entity
{
    public class Translation : Entity<int>
    {
        /// <summary>Gets or sets the original word.</summary>
        public string Word { get; set; }

        /// <summary>Gets or sets translation of the word.</summary>
        public string Translate { get; set; }

        /// <summary>Gets or sets the date when translation was added.</summary>
        public DateTime AddDate { get; set; }
    }
}