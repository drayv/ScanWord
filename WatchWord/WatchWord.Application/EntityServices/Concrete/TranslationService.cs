using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Concrete
{
    /// <summary>Represents a layer for work with translator and dictionaries api.</summary>
    public class TranslationService : ITranslationService
    {
        private static string _yandexTranslateApiKey;
        private static string _yandexDictionaryApiKey;
        private readonly ISettingsService _settingsService;
        private readonly IWatchWordUnitOfWork _watchWordUnitOfWork;

        /// <summary>Prevents a default instance of the <see cref="TranslationService"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private TranslationService()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TranslationService"/> class.</summary>
        /// <param name="settingsService">Settings service.</param>
        /// <param name="watchWordUnitOfWork">Unit of work over WatchWord repositories.</param>
        public TranslationService(ISettingsService settingsService, IWatchWordUnitOfWork watchWordUnitOfWork)
        {
            _settingsService = settingsService;
            _watchWordUnitOfWork = watchWordUnitOfWork;
        }

        /// <summary>Gets the list of translations of the word by using cache, ya dict, or ya translate api.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of translations.</returns>
        public IEnumerable<string> GetTranslations(string word)
        {
            var translations = new List<string>();

            // cache
            translations.AddRange(GetTranslateFromCache(word));
            if (translations.Count != 0)
            {
                return translations;
            }

            // yandex dictionary
            var source = TranslationSource.YandexDictionary;
            translations.AddRange(GetYandexDictionaryTranslations(word));
            if (translations.Count == 0)
            {
                // yandex translate
                source = TranslationSource.YandexTranslate;
                translations.AddRange(GetYandexTranslateWord(word));
            }

            SaveTranslationsToCache(word, translations, source);
            return translations;
        }

        /// <summary>Saves translations to the cache.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translations">List of translations.</param>
        /// <param name="source">Source of the translation.</param>
        private void SaveTranslationsToCache(string word, IReadOnlyCollection<string> translations, TranslationSource source)
        {
            Task.Run(() =>
            {
                if (translations.Count == 0) return;

                // delete if exist
                var existing = _watchWordUnitOfWork.TranslationsRepository.GetAll(t => t.Word == word);
                foreach (var translation in existing)
                {
                    _watchWordUnitOfWork.TranslationsRepository.Delete(translation);
                }

                // save translations
                var translationsCache = translations.Select(translation => new Translation
                {
                    Word = word,
                    Translate = translation,
                    AddDate = DateTime.Now,
                    Source = source
                }).ToList();

                _watchWordUnitOfWork.TranslationsRepository.Insert(translationsCache);
                _watchWordUnitOfWork.TranslationsRepository.Save();
            });
        }

        /// <summary>Gets translations list from the cache for specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of the translations.</returns>
        private IEnumerable<string> GetTranslateFromCache(string word)
        {
            return _watchWordUnitOfWork.TranslationsRepository.GetAll(t => t.Word == word).Select(s => s.Translate);
        }

        /// <summary>Gets translations list using yandex translate api for specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of the translations.</returns>
        private IEnumerable<string> GetYandexTranslateWord(string word)
        {
            var translations = new List<string>();

            var address = string.Format("https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&lang={1}&text={2}",
            Uri.EscapeDataString(GetYandexTranslateApiKey()),
            Uri.EscapeDataString("en-ru"),
            Uri.EscapeDataString(word));

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            var httpResponse = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
            if (httpResponse == null) return translations;

            string text;
            using (var streamReader = new StreamReader(httpResponse))
            {
                text = streamReader.ReadToEnd();
            }

            var yandexTranslateWords = JsonConvert.DeserializeObject<YandexTranslateWords>(text);
            translations.AddRange(yandexTranslateWords.text);

            return translations;
        }

        /// <summary>Gets translations list using yandex dictionary api for specified word.</summary>
        /// <param name="word">Specified word.</param>
        /// <returns>List of the translations.</returns>
        private IEnumerable<string> GetYandexDictionaryTranslations(string word)
        {
            var translations = new List<string>();

            var address = string.Format("https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key={0}&lang={1}&text={2}",
            Uri.EscapeDataString(GetYandexDictionaryApiKey()),
            Uri.EscapeDataString("en-ru"),
            Uri.EscapeDataString(word));

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            var httpResponse = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
            if (httpResponse == null) return translations;

            string text;
            using (var streamReader = new StreamReader(httpResponse))
            {
                text = streamReader.ReadToEnd();
            }

            var yandexDictionaryTranslateWords = JsonConvert.DeserializeObject<YandexDictionaryTranslateWords>(text);
            foreach (var partOfSpeech in yandexDictionaryTranslateWords.def)
            {
                translations.AddRange(partOfSpeech.tr.Select(translate => translate.text));
            }

            return translations;
        }

        /// <summary>Gets yandex dictionary api key from data storage.</summary>
        /// <returns>Yandex dictionary api key.</returns>
        private string GetYandexDictionaryApiKey()
        {
            if (!string.IsNullOrEmpty(_yandexDictionaryApiKey)) return _yandexDictionaryApiKey;
            var setting = _settingsService.GetSiteSetting(SettingKey.YandexDictionaryApiKey);
            if (setting != null)
            {
                _yandexDictionaryApiKey = _settingsService.GetSiteSetting(SettingKey.YandexDictionaryApiKey).String;
            }
            else
            {
                throw new Exception("Yandex dictionary api key not found.");
            }

            return _yandexDictionaryApiKey;
        }

        /// <summary>Gets yandex translate api key from data storage.</summary>
        /// <returns>Yandex translate api key.</returns>
        private string GetYandexTranslateApiKey()
        {
            if (!string.IsNullOrEmpty(_yandexTranslateApiKey)) return _yandexTranslateApiKey;
            var setting = _settingsService.GetSiteSetting(SettingKey.YandexTranslateApiKey);
            if (setting != null)
            {
                _yandexTranslateApiKey = _settingsService.GetSiteSetting(SettingKey.YandexTranslateApiKey).String;
            }
            else
            {
                throw new Exception("Yandex translate api key not found.");
            }

            return _yandexTranslateApiKey;
        }

        #region Yandex Classes

        protected class Head
        {
        }

        protected class Tr2
        {
            // ReSharper disable InconsistentNaming
            public string text { get; set; }
        }

        protected class Ex
        {
            public string text { get; set; }
            public List<Tr2> tr { get; set; }
        }

        protected class Mean
        {
            public string text { get; set; }
        }

        protected class Tr
        {
            public string text { get; set; }
            public string pos { get; set; }
            public List<Ex> ex { get; set; }
            public List<Mean> mean { get; set; }
        }

        protected class Def
        {
            public string text { get; set; }
            public string pos { get; set; }
            public string ts { get; set; }
            public List<Tr> tr { get; set; }
        }

        protected class YandexDictionaryTranslateWords
        {
            public Head head { get; set; }
            public List<Def> def { get; set; }
        }

        public class YandexTranslateWords
        {
            public int code { get; set; }
            public string lang { get; set; }
            public List<string> text { get; set; }
            // ReSharper restore InconsistentNaming
        }

        #endregion
    }
}