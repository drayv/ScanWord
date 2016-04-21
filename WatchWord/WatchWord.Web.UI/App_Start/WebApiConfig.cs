using System.Web.Http;

class WebApiConfig
{
    public static void Register(HttpConfiguration configuration)
    {
        configuration.MapHttpAttributeRoutes();
        configuration.Routes.MapHttpRoute("DefaultAPI", "api/{controller}/{id}",
            new { id = RouteParameter.Optional });
    }
}