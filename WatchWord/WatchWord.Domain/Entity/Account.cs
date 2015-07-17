namespace WatchWord.Domain.Entity
{
    /// <summary>WatchWord user account entity.</summary>
    public class Account
    {
        /// <summary>Gets or sets WatchWord account Id.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets External account Id (Identity framework, e.t.c.).</summary>
        public int ExternalId { get; set; }
    }
}