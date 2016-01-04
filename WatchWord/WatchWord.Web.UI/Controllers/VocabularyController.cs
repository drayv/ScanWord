using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Web.UI.Models.Vocabulary;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WatchWord.Web.UI.Controllers
{
    public class VocabularyController : AsyncController
    {
        private readonly IVocabularyService _vocabularyService;

        /// <summary>Initializes a new instance of the <see cref="VocabularyController"/> class.</summary>
        /// <param name="vocabularyService">Vocabulary service.</param>
        public VocabularyController(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }

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

        [HttpPost]
        [Authorize]
        public JsonResult InsertKnownWord(string word, string translation)
        {
            var result = _vocabularyService.InsertKnownWord(word, translation, User.Identity.GetUserId<int>());
            return Json(result > 0 ? "success, entities: " + result : "error");
        }

        [HttpPost]
        [Authorize]
        public JsonResult InsertLearnWord(string word, string translation)
        {
            var result = _vocabularyService.InsertLearnWord(word, translation, User.Identity.GetUserId<int>());
            return Json(result > 0 ? "success, entities: " + result : "error");
        }
    }
}