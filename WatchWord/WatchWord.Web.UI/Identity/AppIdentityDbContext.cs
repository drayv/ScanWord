using Microsoft.AspNet.Identity.EntityFramework;
using WatchWord.Web.UI.Models.Identity;

namespace WatchWord.Web.UI.Identity
{
    /// <summary>Identity database context.</summary>
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        /// <summary>Initializes a new instance of the <see cref="AppIdentityDbContext"/> class.</summary>
        /// <param name="dataBaseName">The database name.</param>
        public AppIdentityDbContext(string dataBaseName)
            : base(dataBaseName)
        {
        }

        /// <summary>Create identity database context.</summary>
        /// <returns>The <see cref="AppIdentityDbContext"/>.</returns>
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext("WatchWord");
        }
    }
}