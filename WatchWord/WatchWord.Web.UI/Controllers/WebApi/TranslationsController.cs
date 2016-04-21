using System.Collections.Generic;
using System.Web.Http;
using WatchWord.Application.EntityServices.Abstract;

namespace WatchWord.Web.UI.Controllers.WebApi
{
    public class TranslationsController : ApiController
    {
        private readonly ITranslationService _translationService;

        /// <summary>Initializes a new instance of the <see cref="TranslationsController"/> class.</summary>
        /// <param name="translationService">Translations service.</param>
        public TranslationsController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        /// <summary>Gets translations of the specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of translations.</returns>
        public IEnumerable<string> Get(string word)
        {
            return _translationService.GetTranslations(word);
        }
    }
}
