using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

using WatchWord.Domain;
using WatchWord.Web.UI.Models.Identity;

namespace WatchWord.Web.UI.Identity
{
    using WatchWord.Infrastructure;

    /// <summary>
    /// Identity database context.
    /// </summary>
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// Unity container.
        /// </summary>
        private static readonly IUnityContainer Container = WatchUnityConfig.GetConfiguredContainer();

        /// <summary>
        /// Initializes a new instance of the <see cref="AppIdentityDbContext"/> class.
        /// </summary>
        /// <param name="dataBaseName">
        /// The database name.
        /// </param>
        public AppIdentityDbContext(string dataBaseName)
            : base(dataBaseName)
        {
        }

        /// <summary>
        /// Create identity database context.
        /// </summary>
        /// <returns>
        /// The <see cref="AppIdentityDbContext"/>.
        /// </returns>
        public static AppIdentityDbContext Create()
        {
            var projectSettings = Container.Resolve<IWatchProjectSettings>();
            return new AppIdentityDbContext(projectSettings.DataBaseName);
        }
    }
}