using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ScanWord.Core.Common;
using ScanWord.Core.Entity;
using WatchWord.Web.UI.Models.Materials;

namespace WatchWord.Web.UI.Controllers
{
    /// <summary>
    /// The materials controller.
    /// </summary>
    public class MaterialsController : Controller
    {
        /// <summary>
        /// The parser.
        /// </summary>
        private readonly IScanWordParser parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialsController"/> class.
        /// </summary>
        /// <param name="parser">
        /// ScanWord parser.
        /// </param>
        public MaterialsController(IScanWordParser parser)
        {
            this.parser = parser;
        }

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

            using (var streamReader = new StreamReader(model.File.InputStream))
            {
                var file = new ScanWord.Core.Entity.File { Path = "TODO", Filename = model.File.FileName, Extension = "TODO" };
                var existingWords = new ConcurrentBag<Word>();
                var composition = parser.ParseFile(file, existingWords, streamReader);
                model.Words = composition.GroupBy(w => w.Word.TheWord).Select(c => c.Key).AsEnumerable();
            }

            // TODO: Add database logic and Redirect to the material page.
            return View(model);
        }
    }
}