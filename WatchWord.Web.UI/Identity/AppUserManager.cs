namespace WatchWord.Web.UI.Identity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using WatchWord.Web.UI.Models.Identity;

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
        /// The create.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="AppUserManager"/>.
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