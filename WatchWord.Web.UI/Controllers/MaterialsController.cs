namespace WatchWord.Web.UI.Controllers
{
    using System.IO;
    using System.Web.Mvc;

    using WatchWord.Web.UI.Models.Materials;

    /// <summary>
    /// The materials controller.
    /// </summary>
    public class MaterialsController : Controller
    {
        /// <summary>
        /// Creates a form for adding a new material to the site.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Add()
        {
            var addViewModel = new AddMaterialViewModel();
            return View(addViewModel);
        }

        /// <summary>
        /// HttpPost of the AddViewModel.
        /// </summary>
        /// <param name="model">
        /// The AddViewModel.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Add(AddMaterialViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            using (var memoryStream = new MemoryStream())
            {
                model.File.InputStream.CopyTo(memoryStream);
            }

            // TODO: Redirect to the material page.
            return View();
        }
    }
}