using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Web.UI.Models.Vocabulary;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WatchWord.Web.UI.Controllers
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

        /// <summary>Inserts word to the user's vocabulary of known words.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translation">Translation of the word.</param>
        /// <returns>JSON with result data.</returns>
        [HttpPost]
        [Authorize]
        public JsonResult InsertKnownWord(string word, string translation)
        {
            var result = _vocabularyService.InsertKnownWord(word, translation, User.Identity.GetUserId<int>());
            return Json(result > 0 ? "success, entities: " + result : "error");
        }

        /// <summary>Inserts word to the user's vocabulary of learning words.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translation">Translation of the word.</param>
        /// <returns>JSON with result data.</returns>
        [HttpPost]
        [Authorize]
        public JsonResult InsertLearnWord(string word, string translation)
        {
            var result = _vocabularyService.InsertLearnWord(word, translation, User.Identity.GetUserId<int>());
            return Json(result > 0 ? "success, entities: " + result : "error");
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