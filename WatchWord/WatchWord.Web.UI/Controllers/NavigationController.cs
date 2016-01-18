using System.Web.Mvc;

namespace WatchWord.Web.UI.Controllers
{
    /// <summary>The navigation bar controller.</summary>
    public class NavigationController : Controller
    {
        /// <summary>Search action.</summary>
        /// <returns>Search results page <see cref="ActionResult"/>.</returns>
        public ActionResult Search()
        {
            return View();
        }
    }
}