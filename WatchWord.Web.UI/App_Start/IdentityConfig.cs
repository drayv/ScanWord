namespace WatchWord.Web.UI
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using WatchWord.Web.UI.Identity;
    using Microsoft.Owin;

    /// <summary>
    /// Identity framework config.
    /// </summary>
    public class IdentityConfig
    {
        /// <summary>
        /// Set configuration for identity framework.
        /// </summary>
        /// <param name="app">
        /// OWIN application builder.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Account/Login")
                });
        }
    }
}