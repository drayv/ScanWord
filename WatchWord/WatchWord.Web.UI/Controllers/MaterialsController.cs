﻿using System.Threading.Tasks;
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

        /// <summary>Initializes a new instance of the <see cref="MaterialsController"/> class.</summary>
        /// <param name="service">Material service.</param>
        public MaterialsController(IMaterialsService service)
        {
            _service = service;
        }

        public ActionResult Material(int id)
        {
            var testMaterial = _service.GetMaterial(id);
            if (testMaterial == null || testMaterial.Equals(default(Material)))
            {
                return RedirectToAction("All"); // TODO: change to main page redirect
            }

            return View(testMaterial);
        }

        /// <summary>Gets all material.</summary>
        /// <param name="startIndex">Number of materials to skip.</param>
        /// <param name="pageSize">Number of materials to take.</param>
        /// <returns></returns>
        public async Task<ActionResult> All(int startIndex = 0, int pageSize = 20)
        {
            var allMaterialsModel = new MaterialsViewModel
            {
                Title = "All materials",
                Materials = await _service.GetMaterials(startIndex, pageSize)
            };

            return View("MaterialsList", allMaterialsModel);
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
        /// <returns>The Save material form if model is valid, or ParseMaterial if not.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult ParseMaterial(ParseMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: get userId
            var material = _service.CreateMaterial(model.File.InputStream, model.Image.InputStream, model.Type, model.Name, model.Description, 0);

            TempData["SaveMaterialModel"] = material; //TODO: fix this to redirect to save action with model
            return RedirectToAction("Save");
        }

        /// <summary>Saves parsed material.</summary>
        /// <returns>Save material form.</returns>
        [Authorize]
        public ActionResult Save()
        {
            var material = TempData["SaveMaterialModel"] as Material;
            TempData["SaveMaterialModel"] = material;

            return View(material);
        }

        //// Test method, while ajax method don't exist. Delete this after job done.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Save(Material material)
        {
            //TODO fix this when ajax done.
            if (TempData["SaveMaterialModel"] != null)
            {
                material = TempData["SaveMaterialModel"] as Material;
            }

            await _service.SaveMaterial(material); //TODO: and redirect to material.
            return View(material);
        }
    }
}