using System.Web.Mvc;
using System.Web.Routing;

namespace WatchWord.Web.UI
{
    /// <summary>The route config.</summary>
    public class RouteConfig
    {
        /// <summary>Register routes.</summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "{controller}/{action}", new { controller = "Materials", action = "ParseMaterial" });

            routes.MapRoute("AllMaterials", "Materials/All/{startIndex}", new
            {
                controller = "Materials",
                action = "All",
                startIndex = 0,
                pageSize = 20
            });
        }
    }
}