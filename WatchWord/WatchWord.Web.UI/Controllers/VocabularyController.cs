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

        /// <summary>Initializes a new instance of the <see cref="VocabularyController"/> class.</summary>
        /// <param name="vocabularyService">Vocabulary service.</param>
        public VocabularyController(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }

        /// <summary>Shows all words from all user dictionaries.</summary>
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

        /// <summary>Inserts word to the user vocabulary of known words.</summary>
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

        /// <summary>Inserts word to the user vocabulary of known words.</summary>
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
    }
}