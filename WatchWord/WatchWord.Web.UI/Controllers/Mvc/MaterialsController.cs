﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.Entity;
using WatchWord.Web.UI.Models.Materials;

namespace WatchWord.Web.UI.Controllers.Mvc
{
    /// <summary>Materials controller.</summary>
    public class MaterialsController : AsyncController
    {
        private readonly IMaterialsService _materialService;
        private readonly IVocabularyService _vocabularyService;
        private const int PageSize = 8;

        //TODO: Fix this. sizes sets in FE
        private const int ImageMaxWidth = 190;
        private const int ImageMaxHeight = 280;

        /// <summary>Initializes a new instance of the <see cref="MaterialsController"/> class.</summary>
        /// <param name="materialService">Material service.</param>
        /// <param name="vocabularyService">Vocabulary service.</param>
        public MaterialsController(IMaterialsService materialService, IVocabularyService vocabularyService)
        {
            _materialService = materialService;
            _vocabularyService = vocabularyService;
        }

        /// <summary>Shows material by specified Id.</summary>
        /// <param name="id">Material identifier.</param>
        /// <returns>Material page.</returns>
        public async Task<ActionResult> Material(int id)
        {
            var material = _materialService.GetMaterialWithWords(id);
            if (material == null)
            {
                return RedirectToAction("DisplayAll"); // TODO: change to main page redirect.
            }

            var vocabWords = await FillVocabWordsByMaterial(material); //TODO: do not take words to material.

            var imgSrc = GetMaterialImageUrl(material.Id);
            return View(new MaterialViewModel(material, vocabWords, ImageMaxWidth, ImageMaxHeight, imgSrc));
        }

        /// <summary>Shows all types of materials on the page.</summary>
        /// <param name="pageNumber">The number of current page.</param>
        /// <returns>Materials list page.</returns>
        public async Task<ActionResult> DisplayAll(int pageNumber = 1)
        {
            var materials = await _materialService.GetMaterialsWithFile(pageNumber, PageSize);
            var statistic = materials.ToDictionary(m => m.Id, m => new MaterialStatisticViewModel
            {
                AllWordsCount = _materialService.WordsCountInMaterial(m)
            });

            var model = new DisplayAllViewModel(PageSize, pageNumber, _materialService.TotalCount(), materials, statistic);
            return View(model);
        }

        /// <summary>Represents form for parsing material.</summary>
        /// <returns>Parsing material form.</returns>
        [Authorize]
        public ActionResult ParseMaterial()
        {
            return View(new ParseMaterialViewModel());
        }

        /// <summary>Checks parsed material before open pre-save page.</summary>
        /// <param name="model">The parsed material view model.</param>
        /// <returns>Pre-save material form if model is valid, or ParseMaterial if not.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult ParseMaterial(ParseMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var material = _materialService.CreateMaterial(model.File.InputStream, model.Image.InputStream, model.Image.ContentType,
                model.Type, model.Name, model.Description, User.Identity.GetUserId<int>(), ImageMaxWidth, ImageMaxHeight);

            TempData["MaterialModel"] = material;
            return RedirectToAction("Save");
        }

        /// <summary>Opens pre-save material page.</summary>
        /// <returns>Material page.</returns>
        [Authorize]
        public async Task<ActionResult> Save()
        {
            var material = TempData.Peek("MaterialModel") as Material;
            if (material == null) return RedirectToAction("ParseMaterial");
            var vocabWords = await FillVocabWordsByMaterial(material);

            var imgSrc = GetMaterialImageUrl(material.Id);
            return View(new MaterialViewModel(material, vocabWords, ImageMaxWidth, ImageMaxHeight, imgSrc));
        }

        /// <summary>Saves material to the storage.</summary>
        /// <returns>Materials list page redirect.</returns>
        [HttpPost]
        [Authorize]
        [ActionName("Save")]
        public async Task<ActionResult> SaveParsedMaterial()
        {
            object material;
            if (!TempData.TryGetValue("MaterialModel", out material)) return RedirectToAction("ParseMaterial");
            var saveMaterial = material as Material;
            if (saveMaterial == null) return RedirectToAction("ParseMaterial");
            await _materialService.SaveMaterial((Material)material);
            return RedirectToAction("DisplayAll"); // TODO: redirect to material page.
        }

        /// <summary>Fills the list of words with translation.</summary>
        /// <param name="material">Material.</param>
        /// <returns>List of vocabulary words.</returns>
        private async Task<List<VocabWord>> FillVocabWordsByMaterial(Material material)
        {
            var arrayOfWords = material.File.Words == null
                ? new string[0]
                : material.File.Words.Select(n => n.TheWord).ToArray();

            return await _vocabularyService.FillVocabByWords(arrayOfWords, User.Identity.GetUserId<int>());
        }

        private string GetMaterialImageUrl(int materialId)
        {
            var imageUrl = "";
            if (Request != null && Request.Url != null)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                imageUrl = new Uri(Request.Url, Url.HttpRouteUrl("DefaultAPI", new { controller = "MaterialImage", id = materialId })).AbsoluteUri;
            }
            return imageUrl;
        }
    }
}