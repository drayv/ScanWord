using System.Data.Entity;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql
{
    /// <summary>
    /// Represents a Unit Of Work with WatchWord database.
    /// </summary>
    public class WatchDataContainer : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WatchDataContainer"/> class.
        /// </summary>
        /// <param name="dataBaseName">
        /// The data base name.
        /// </param>
        public WatchDataContainer(string dataBaseName)
            : base("name=" + dataBaseName)
        {
        }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the materials.
        /// </summary>
        public DbSet<Material> Materials { get; set; }
    }
}