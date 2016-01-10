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

            routes.MapRoute(null, "Settings", new { controller = "Settings", action = "Index" });

            routes.MapRoute(null, "Materials", new { controller = "Materials", action = "DisplayAll", pageNumber = 1 });

            routes.MapRoute(null, "Vocabulary", new { controller = "Vocabulary", action = "DisplayAll" });

            routes.MapRoute(null, "Materials/Page/{pageNumber}", new { controller = "Materials", action = "DisplayAll" }, new { pageNumber = @"\d+" });

            routes.MapRoute(null, "Material/{id}", new { controller = "Materials", action = "Material" });

            routes.MapRoute(null, "Materials/All/{startIndex}/{pageSize}", new { controller = "Materials", action = "All" });

            routes.MapRoute("MainPage", "{controller}/{action}", new { controller = "Materials", action = "ParseMaterial" }); // TODO: change to main
        }
    }
}