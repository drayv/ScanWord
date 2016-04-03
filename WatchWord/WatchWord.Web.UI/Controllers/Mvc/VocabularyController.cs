using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Web.UI.Models.Vocabulary;

namespace WatchWord.Web.UI.Controllers.Mvc
{
    /// <summary>Vocabulary controller.</summary>
    public class VocabularyController : AsyncController
    {
        private readonly IVocabularyService _vocabularyService;
        private readonly ITranslationService _translationService;

        /// <summary>Initializes a new instance of the <see cref="VocabularyController"/> class.</summary>
        /// <param name="vocabularyService">Vocabulary service.</param>
        /// <param name="translationService">Translation service.</param>
        public VocabularyController(IVocabularyService vocabularyService, ITranslationService translationService)
        {
            _vocabularyService = vocabularyService;
            _translationService = translationService;
        }

        /// <summary>Shows all words from all user's dictionaries.</summary>
        /// <returns>Vocabularies page.</returns>
        [Authorize]
        public async Task<ActionResult> DisplayAll()
        {
            var model = new DisplayAllViewModel
            {
                KnownWords = await _vocabularyService.GetKnownWords(User.Identity.GetUserId<int>()),
                LearnWords = await _vocabularyService.GetLearnWords(User.Identity.GetUserId<int>())
            };

            return View(model);
        }

        /// <summary>Gets translations of the specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>JSON list of translations.</returns>
        [HttpGet]
        public JsonResult GetTranslations(string word)
        {
            var translations = _translationService.GetTranslations(word);
            return Json(translations, JsonRequestBehavior.AllowGet);
        }
    }
}