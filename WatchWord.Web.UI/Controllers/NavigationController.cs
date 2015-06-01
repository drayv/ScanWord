namespace WatchWord.Web.UI.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The navigation bar controller.
    /// </summary>
    public class NavigationController : Controller
    {
        /// <summary>
        /// The search.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Search()
        {
            return View();
        }
    }
}