namespace ScanWord.Web.UI
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The model view controller application.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
