using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchWord.Web.UI.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WatchWord.Web.UI.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {

        }

        /// <summary>
        /// Creates an instance of the AppUserManager
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var db = context.Get<AppIdentityDbContext>();
            var userManager = new AppUserManager(new UserStore<AppUser>(db));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
            };
            userManager.UserValidator = new UserValidator<AppUser>(userManager)
            {
                RequireUniqueEmail = true
            };
            return userManager;
        }
    }
}