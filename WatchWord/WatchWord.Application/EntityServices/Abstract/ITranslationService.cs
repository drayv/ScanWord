using System.Collections.Generic;

namespace WatchWord.Application.EntityServices.Abstract
{
    public interface ITranslationService
    {
        IEnumerable<string> GetTranslations(string word);
    }
}