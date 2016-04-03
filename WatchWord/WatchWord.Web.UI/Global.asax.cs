using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;

namespace WatchWord.Web.UI
{
    /// <summary>The model-view-controller application.</summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>The application start point.</summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}