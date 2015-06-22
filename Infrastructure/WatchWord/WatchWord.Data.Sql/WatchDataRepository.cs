using WatchWord.Domain;
using WatchWord.Domain.Data;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql
{
    /// <summary>
    /// Provides CRUD operations for WatchWord database.
    /// </summary>
    public class WatchDataRepository : IWatchDataRepository
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        private readonly string dataBaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="WatchDataRepository"/> class.
        /// </summary>
        /// <param name="settings">The data base name.</param>
        public WatchDataRepository(IProjectSettings settings)
        {
            this.dataBaseName = settings.DataBaseName;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="WatchDataRepository"/> class from being created.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private WatchDataRepository()
        {
        }

        /// <summary>
        /// Add the account to database.
        /// </summary>
        /// <param name="account">The account.</param>
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
        /// <param name="material">The material.</param>
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