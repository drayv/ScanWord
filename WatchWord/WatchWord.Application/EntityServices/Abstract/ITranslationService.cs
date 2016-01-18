using System.Collections.Generic;

namespace WatchWord.Application.EntityServices.Abstract
{
    /// <summary>Represents a layer for work with translator and dictionaries api.</summary>
    public interface ITranslationService
    {
        // <summary>Gets the list of translations of the word by using cache, ya dict, or ya translate api.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of translations.</returns>
        IEnumerable<string> GetTranslations(string word);
    }
}