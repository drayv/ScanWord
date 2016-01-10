using System;
using System.Collections.Generic;
using System.Linq;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Concrete
{
    public class SettingsService : ISettingsService
    {
        private readonly IWatchWordUnitOfWork _watchWordUnitOfWork;
        private readonly IAccountService _accountService;

        private static readonly Dictionary<SettingKey, SettingType> SettingKeyToTypeMapping
            = new Dictionary<SettingKey, SettingType>
            {
                {SettingKey.YandexDictionaryApiKey, SettingType.String},
                {SettingKey.YandexTranslateApiKey, SettingType.String}
            };

        private static readonly List<SettingKey> AdminSettingsKeys
            = new List<SettingKey>
            {
                SettingKey.YandexDictionaryApiKey,
                SettingKey.YandexTranslateApiKey
            };

        /// <summary>Prevents a default instance of the <see cref="MaterialsService"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private SettingsService()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MaterialsService"/> class.</summary>
        /// <param name="watchWordUnitOfWork">Unit of work over WatchWord repositories.</param>
        /// <param name="accountService">Account service.</param>
        public SettingsService(IWatchWordUnitOfWork watchWordUnitOfWork, IAccountService accountService)
        {
            _watchWordUnitOfWork = watchWordUnitOfWork;
            _accountService = accountService;
        }

        public List<Setting> GetUnfilledAdminSettings()
        {
            var filledAdminSettings = _watchWordUnitOfWork.SettingsRepository.GetAll(s => AdminSettingsKeys.Contains(s.Key));
            var unfilledAdminSettings = (from settingKey in AdminSettingsKeys
                                         where filledAdminSettings.All(s => s.Key != settingKey)
                                         select CreateNewSettingByKey(settingKey)).ToList();

            return unfilledAdminSettings;
        }

        public int InsertAdminSettings(List<Setting> settings)
        {
            //TODO: universal parser
            foreach (var setting in settings)
            {
                if (setting.Type == SettingType.String && !string.IsNullOrEmpty(setting.String))
                {
                    _watchWordUnitOfWork.SettingsRepository.Insert(setting);
                }
            }

            var result = _watchWordUnitOfWork.Commit();
            return result;
        }

        public Setting GetAdminSetting(SettingKey key)
        {
            return _watchWordUnitOfWork.SettingsRepository.GetByСondition(s => s.Key == key);
        }

        private static Setting CreateNewSettingByKey(SettingKey key)
        {
            var newSetting = new Setting { Key = key };

            var mapping = SettingKeyToTypeMapping.First(m => m.Key == key);
            newSetting.Type = mapping.Value;

            return newSetting;
        }
    }
}