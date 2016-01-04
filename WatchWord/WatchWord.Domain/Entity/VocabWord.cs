using ScanWord.Core.Entity.Common;

namespace WatchWord.Domain.Entity
{
    /// <summary>The word from vocabulary of words.</summary>
    public class VocabWord : Entity<int>
    {
        /// <summary>Gets or sets the original word.</summary>
        public string Word { get; set; }

        /// <summary>Gets or sets translation of the word.</summary>
        public string Translation { get; set; }

        /// <summary>Gets or sets the owner of the vocabulary word.</summary>
        public Account Owner { get; set; }
    }
}