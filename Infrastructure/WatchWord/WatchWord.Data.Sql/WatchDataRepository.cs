using WatchWord.Domain.Data;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql
{
    /// <summary>
    /// Provides logic for working with database.
    /// </summary>
    public class WatchDataRepository : IWatchDataRepository
    {
        /// <summary>
        /// Gets or sets the data base name.
        /// </summary>
        private readonly string dataBaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="WatchDataRepository"/> class.
        /// </summary>
        /// <param name="dataBaseName">
        /// The data base name.
        /// </param>
        public WatchDataRepository(string dataBaseName)
        {
            this.dataBaseName = dataBaseName;
        }

        /// <summary>
        /// Add the account to database.
        /// </summary>
        /// <param name="account">
        /// The account.
        /// </param>
        public void AddAccount(Account account)
        {
            using (var db = new WatchDataContainer(this.dataBaseName))
            {
                db.Accounts.Add(account);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add the material to database.
        /// </summary>
        /// <param name="material">
        /// The material.
        /// </param>
        public void AddMaterial(Material material)
        {
            using (var db = new WatchDataContainer(this.dataBaseName))
            {
                db.Materials.Add(material);
                db.SaveChanges();
            }
        }
    }
}