using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
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

            routes.MapRoute(null, "Materials", new { controller = "Materials", action = "DisplayAll", pageNumber = 1 });

            routes.MapRoute(null, "Materials/Page/{pageNumber}", new { controller = "Materials", action = "DisplayAll" }, new { pageNumber = @"\d+" });

            routes.MapRoute("MainPage", "{controller}/{action}", new { controller = "Materials", action = "ParseMaterial" }); // TODO: change to main

            routes.MapRoute("MaterialById", "Materials/Material/{id}", new { controller = "Materials", action = "Material" });

            routes.MapRoute("AllMaterialsWithIndexAndPageSize", "Materials/All/{startIndex}/{pageSize}", new { controller = "Materials", action = "All" });
        }
    }
}