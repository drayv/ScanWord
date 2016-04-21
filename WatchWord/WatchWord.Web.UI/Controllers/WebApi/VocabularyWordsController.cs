using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WatchWord.Application.EntityServices.Abstract;

namespace WatchWord.Web.UI.Controllers.WebApi
{
    public class VocabularyWordsController : ApiController
    {
        private readonly IVocabularyService _vocabularyService;

        /// <summary>Initializes a new instance of the <see cref="VocabularyWordsController"/> class.</summary>
        /// <param name="vocabularyService">Vocabulary service.</param>
        public VocabularyWordsController(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }

        // POST api/VocabularyWords
        /// <summary>Inserts word to the user's vocabulary of known or learning words.</summary>
        /// <param name="vocabAction"><see cref="VocabActionDto"/></param>
        [Authorize]
        public string Post([FromBody]VocabActionDto vocabAction)
        {
            int result;
            if (vocabAction.IsKnown)
            {
                result = _vocabularyService.InsertKnownWord(vocabAction.Word,
                    vocabAction.Translation, User.Identity.GetUserId<int>());
            }
            else
            {
                result = _vocabularyService.InsertLearnWord(vocabAction.Word,
                    vocabAction.Translation, User.Identity.GetUserId<int>());
            }

            if (result > 0)
            {
                return "OK";
            }

            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                "Word was not inserted into vocabulary."));
        }

        public class VocabActionDto
        {
            public string Word { get; set; }
            public string Translation { get; set; }
            public bool IsKnown { get; set; }
        }
    }
}