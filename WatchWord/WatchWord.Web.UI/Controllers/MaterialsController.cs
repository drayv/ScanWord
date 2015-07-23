using System.Threading.Tasks;
using System.Web.Mvc;
using WatchWord.Domain.Common;
using WatchWord.Domain.Entity;
using WatchWord.Web.UI.Models.Materials;

namespace WatchWord.Web.UI.Controllers
{
    /// <summary>The materials controller.</summary>
    public class MaterialsController : Controller
    {
        /// <summary>The parser.</summary>
        private readonly IMaterialsService _service;

        /// <summary>Initializes a new instance of the <see cref="MaterialsController"/> class.</summary>
        /// <param name="service">material service.</param>
        public MaterialsController(IMaterialsService service)
        {
            _service = service;
        }

        /// <summary>Index action</summary>
        /// <returns>The <see cref="ActionResult"/>View which displays form for file search on user's computer.</returns>
        public ActionResult ParseMaterial()
        {
            return View();
        }

        /// <summary>HttpPost of the AddViewModel.</summary>
        /// <param name="model">The AddViewModel.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        public ActionResult ParseMaterial(ParseMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: get userId
            var material = _service.CreateMaterial(model.File.InputStream, model.Type, model.Name, model.Description, 0);

            TempData["SaveMaterialModel"] = material; //TODO: fix this to redirect to save action with model
            return RedirectToAction("Save");
        }

        public ActionResult Save()
        {
            var material = TempData["SaveMaterialModel"] as Material;
            TempData["SaveMaterialModel"] = material;

            if (material == null || material.Compositions == null || material.Compositions.Count == 0)
            {
                return RedirectToAction("Add");
            }

            return View(material);
        }

        //// Test method, while ajax method don't exist. Delete this after job done.
        [HttpPost]
        public async Task<ActionResult> Save(Material material)
        {
            //TODO fix this when ajax done.
            if (TempData["SaveMaterialModel"] != null)
            {
                material = TempData["SaveMaterialModel"] as Material;
            }

            if (material == null || material.Compositions == null || material.Compositions.Count == 0)
            {
                return RedirectToAction("Add");
            }

            await _service.SaveMaterial(material); //TODO: and redirect to material.
            return View(material);
        }
    }
}