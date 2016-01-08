using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.Entity;
using WatchWord.Web.UI.Models.Materials;

namespace WatchWord.Web.UI.Controllers
{
    /// <summary>Materials controller.</summary>
    public class MaterialsController : AsyncController
    {
        private readonly IMaterialsService _service;
        private const int ImageMaxWidth = 190;
        private const int ImageMaxHeight = 280;
        private const int PageSize = 10;

        /// <summary>Initializes a new instance of the <see cref="MaterialsController"/> class.</summary>
        /// <param name="service">Material service.</param>
        public MaterialsController(IMaterialsService service)
        {
            _service = service;
        }

        /// <summary>Shows material by specified Id.</summary>
        /// <param name="id">Material identifier.</param>
        /// <returns>Material page.</returns>
        public ActionResult Material(int id)
        {
            var testMaterial = _service.GetMaterial(id);
            if (testMaterial == null)
            {
                return RedirectToAction("DisplayAll"); // TODO: change to main page redirect.
            }

            return View(new MaterialViewModel(testMaterial, ImageMaxWidth, ImageMaxHeight));
        }

        /// <summary>Shows all types of materials on the page.</summary>
        /// <param name="pageNumber">The number of current page.</param>
        /// <returns>Materials list page.</returns>
        public async Task<ActionResult> DisplayAll(int pageNumber = 1)
        {
            var model = new DisplayAllViewModel(PageSize, pageNumber, _service.TotalCount(), await _service.GetMaterials(pageNumber, PageSize));
            return View(model);
        }

        /// <summary>Represents form for parse material.</summary>
        /// <returns>Parse material form.</returns>
        [Authorize]
        public ActionResult ParseMaterial()
        {
            return View(new ParseMaterialViewModel());
        }

        /// <summary>Checks parsed material before save it.</summary>
        /// <param name="model">The parsed material view model.</param>
        /// <returns>Save material form if model is valid, or ParseMaterial if not.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult ParseMaterial(ParseMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var material = _service.CreateMaterial(model.File.InputStream, model.Image.InputStream, model.Type, model.Name,
                model.Description, User.Identity.GetUserId<int>(), ImageMaxWidth, ImageMaxHeight);

            TempData["MaterialModel"] = material;
            return RedirectToAction("Save");
        }

        /// <summary>Saves parsed material.</summary>
        /// <returns>Save material form.</returns>
        [Authorize]
        public ActionResult Save()
        {
            var material = TempData.Peek("MaterialModel") as Material;
            if (material != null)
            {
                return View(new MaterialViewModel(material, ImageMaxWidth, ImageMaxHeight));
            }

            return RedirectToAction("ParseMaterial");
        }

        /// <summary>Saves material to the storage.</summary>
        /// <returns>Redirects to the materials view.</returns>
        [HttpPost]
        [Authorize]
        [ActionName("Save")]
        public async Task<ActionResult> SaveParsedMaterial()
        {
            object material;
            if (!TempData.TryGetValue("MaterialModel", out material)) return RedirectToAction("ParseMaterial");
            var saveMaterial = material as Material;
            if (saveMaterial == null) return RedirectToAction("ParseMaterial");
            await _service.SaveMaterial((Material)material);
            return RedirectToAction("DisplayAll"); // TODO: redirect to material page.
        }
    }
}