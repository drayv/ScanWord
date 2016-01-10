using System;
using System.IO;
using System.Net;
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

        public string GetTranslations(string word)
        {
            //TODO: translate caching

            var address = string.Format("https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key={0}&lang={1}&text={2}",
            Uri.EscapeDataString(GetYandexDictionaryApiKey()),
            Uri.EscapeDataString("en-ru"),
            Uri.EscapeDataString(word));

            var text = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            var httpResponse = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
            if (httpResponse == null) return text;
            using (var streamReader = new StreamReader(httpResponse))
            {
                text = streamReader.ReadToEnd();
            }

            //TODO: parse dict

            //TODO: yandex translate if dict is null

            return text;
        }

        private string GetYandexDictionaryApiKey()
        {
            if (string.IsNullOrEmpty(_yandexDictionaryApiKey))
            {
                _yandexDictionaryApiKey = _settingsService.GetAdminSetting(SettingKey.YandexDictionaryApiKey).String;
            }

            return _yandexDictionaryApiKey;
        }
    }
}