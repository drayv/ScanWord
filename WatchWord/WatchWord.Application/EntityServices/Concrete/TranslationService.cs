using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Concrete
{
    /// <summary>Represents a layer for work with translator and dictionaries api.</summary>
    public class TranslationService : ITranslationService
    {
        private static string _yandexTranslateApiKey;
        private static string _yandexDictionaryApiKey;
        private readonly ISettingsService _settingsService;

        /// <summary>Prevents a default instance of the <see cref="TranslationService"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private TranslationService()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TranslationService"/> class.</summary>
        /// <param name="settingsService">Settings service.</param>
        public TranslationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public IEnumerable<string> GetTranslations(string word)
        {
            //TODO: translate caching

            var translations = new List<string>();
            translations.AddRange(GetYandexDictionaryTranslations(word));

            if (translations.Count == 0)
            {
                translations.AddRange(GetYandexTranslateWord(word));
            }

            return translations;
        }

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

        private string GetYandexDictionaryApiKey()
        {
            if (string.IsNullOrEmpty(_yandexDictionaryApiKey))
            {
                _yandexDictionaryApiKey = _settingsService.GetSiteSetting(SettingKey.YandexDictionaryApiKey).String;
            }

            return _yandexDictionaryApiKey;
        }

        private string GetYandexTranslateApiKey()
        {
            if (string.IsNullOrEmpty(_yandexTranslateApiKey))
            {
                _yandexTranslateApiKey = _settingsService.GetSiteSetting(SettingKey.YandexTranslateApiKey).String;
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