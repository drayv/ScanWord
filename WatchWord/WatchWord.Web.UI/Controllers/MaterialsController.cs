using System.Threading.Tasks;
using System.Web.Mvc;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.Entity;
using WatchWord.Web.UI.Models.Materials;

namespace WatchWord.Web.UI.Controllers
{
    /// <summary>The materials controller.</summary>
    public class MaterialsController : AsyncController
    {
        /// <summary>The service for work with materials.</summary>
        private readonly IMaterialsService _service;

        private static int width = 190;
        private static int height = 280;

        /// <summary>Initializes a new instance of the <see cref="MaterialsController"/> class.</summary>
        /// <param name="service">Material service.</param>
        public MaterialsController(IMaterialsService service)
        {
            _service = service;
        }

        /// <summary>Gets all material.</summary>
        /// <param name="startIndex">Number of materials to skip.</param>
        /// <param name="pageSize">Number of materials to take.</param>
        /// <returns></returns>
        public async Task<ActionResult> All(int startIndex = 0, int pageSize = 20)
        {
            var allMaterialsModel = new MaterialsModel
            {
                Title = "All materials",
                Materials = await _service.GetMaterials(startIndex, pageSize)
            };

            return View("MaterialsList", allMaterialsModel);
        }

        /// <summary>Represents form for parse material.</summary>
        /// <returns>Parse material form.</returns>
        public ActionResult ParseMaterial()
        {
            return View(new ParseMaterialViewModel());
        }

        /// <summary>Checks parsed material before save it.</summary>
        /// <param name="model">The parsed material view model.</param>
        /// <returns>The Save material form if model is valid, or ParseMaterial if not.</returns>
        [HttpPost]
        public ActionResult ParseMaterial(ParseMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: get userId
            var material = _service.CreateMaterial(model.File.InputStream, model.Image.InputStream, model.Type, model.Name, model.Description, 0, width, height);

            TempData["SaveMaterialModel"] = material;
            return RedirectToAction("Save");
        }

        /// <summary>Saves parsed material.</summary>
        /// <returns>Save material form.</returns>
        public ActionResult Save()
        {
            var material = TempData.Peek("SaveMaterialModel") as Material;
            if(material != null)
                return View(new SaveMaterialViewModel(material, width, height));

            return RedirectToAction("ParseMaterial");
        }

        [HttpPost()]
        [ActionName("Save")]
        public async Task<ActionResult> SaveParsedMaterial()
        {
            object material;
            if(TempData.TryGetValue("SaveMaterialModel", out material) && material is Material)
            {
                await _service.SaveMaterial(material as Material);
                //Redirect to material must be here, so cach your error :c
                return View(material);
            }
            return RedirectToAction("ParseMaterial");
        }
    }
}