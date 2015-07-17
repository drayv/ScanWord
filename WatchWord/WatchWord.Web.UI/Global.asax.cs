using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using WatchWord.Domain.Data;
using WatchWord.Infrastructure;

namespace WatchWord.Web.UI
{
    /// <summary>The model-view-controller application.</summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>The application start point.</summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //// Initialize repository on first start.
            var container = WatchUnityConfig.GetConfiguredContainer();
            var repository = container.Resolve<IWatchDataRepository>();
            repository.GetAccounts(a => a.Id == 0);
        }
    }
}