using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WatchWord.Web.UI.Models.Identity;
using Microsoft.AspNet.Identity;

namespace WatchWord.Web.UI.Identity
{
    /// <summary>Identity database context.</summary>
    [DbConfigurationType(typeof(DataAccess.WatchDbConfiguration))]
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        /// <summary>Initializes a new instance of the <see cref="AppIdentityDbContext"/> class.</summary>
        public AppIdentityDbContext()
            : base("name=WatchWord")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppIdentityDbContext, Migrations.Configuration>());
        }

        /// <summary>Create identity database context.</summary>
        /// <returns>The <see cref="AppIdentityDbContext"/>.</returns>
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

        /// <summary>Configure model with fluent API.</summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => r.);
        }
    }
}