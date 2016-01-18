using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WatchWord.Web.UI.Models.Identity;

namespace WatchWord.Web.UI.Identity
{
    /// <summary>Identity users manager.</summary>
    public class AppUserManager : UserManager<AppUser, int>
    {
        /// <summary>Initializes a new instance of the <see cref="AppUserManager"/> class.</summary>
        /// <param name="store">Identity users store.</param>
        public AppUserManager(IUserStore<AppUser, int> store)
            : base(store)
        {
        }

        /// <summary>Creates application users manager.</summary>
        /// <param name="options">Identity factory options.</param>
        /// <param name="context">OWIN context.</param>
        /// <returns>The application users manager <see cref="AppUserManager"/>.</returns>
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var db = context.Get<AppIdentityDbContext>();
            var userManager = new AppUserManager(new UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>(db))
            {
                PasswordValidator = new PasswordValidator { RequiredLength = 6 }
            };

            userManager.UserValidator = new UserValidator(userManager)
            {
                RequireUniqueEmail = true,
                UserNameMinLength = 2,
                UserNameStartsWithDigit = false
            };

            return userManager;
        }
    }
}