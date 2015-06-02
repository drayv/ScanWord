using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WatchWord.Web.UI.Models.Identity;

namespace WatchWord.Web.UI.Identity
{
    /// <summary>
    /// Identity user manager.
    /// </summary>
    public class AppUserManager : UserManager<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserManager"/> class.
        /// </summary>
        /// <param name="store">
        /// Identity user store.
        /// </param>
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {
        }

        /// <summary>
        /// Creates application user manager.
        /// </summary>
        /// <param name="options">
        /// Identity factory options.
        /// </param>
        /// <param name="context">
        /// OWIN context.
        /// </param>
        /// <returns>
        /// The application user manager <see cref="AppUserManager"/>.
        /// </returns>
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var db = context.Get<AppIdentityDbContext>();
            var userManager = new AppUserManager(new UserStore<AppUser>(db))
                {
                    PasswordValidator = new PasswordValidator { RequiredLength = 6 }
                };

            userManager.UserValidator = new ScanWordUserValidator(userManager)
            {
                RequireUniqueEmail = true,
                UserNameMinLength = 4,
                UserNameStartsWithDigit = false
            };

            return userManager;
        }
    }
}