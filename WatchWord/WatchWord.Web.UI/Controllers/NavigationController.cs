using System.Web.Mvc;

namespace WatchWord.Web.UI.Controllers
{
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