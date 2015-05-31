using Owin;
using WatchWord.Web.UI.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace WatchWord.Web.UI
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AppIdentityDbContext>(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
                }
            );
        }
    }
}
