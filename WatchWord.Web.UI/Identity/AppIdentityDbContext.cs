namespace WatchWord.Web.UI.Identity
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using WatchWord.Web.UI.Models.Identity;

    /// <summary>
    /// Identity database context.
    /// </summary>
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppIdentityDbContext"/> class.
        /// </summary>
        public AppIdentityDbContext()
            : base("WatchWord")
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
            return new AppIdentityDbContext();
        }
    }
}