using System.Linq;
using System.Collections.Generic;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Concrete
{
    /// <summary>Represents a layer for work with settings.</summary>
    public class SettingsService : ISettingsService
    {
        private readonly IWatchWordUnitOfWork _watchWordUnitOfWork;
        private readonly IAccountService _accountService;

        /// <summary>These keys contain the specified data types.</summary>
        private static readonly Dictionary<SettingKey, SettingType> SettingKeyToTypeMapping
            = new Dictionary<SettingKey, SettingType>
            {
                {SettingKey.YandexDictionaryApiKey, SettingType.String},
                {SettingKey.YandexTranslateApiKey, SettingType.String}
            };

        /// <summary>These keys are responsible for the configuration of the site.</summary>
        private static readonly List<SettingKey> SiteSettingsKeys
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

        /// <summary>Gets the site configuration settings, which is not yet filled.</summary>
        /// <returns>List of the site configuration settings.</returns>
        public List<Setting> GetUnfilledSiteSettings()
        {
            var filledAdminSettings = _watchWordUnitOfWork.SettingsRepository.GetAll(s => SiteSettingsKeys.Contains(s.Key));
            var unfilledAdminSettings = (from settingKey in SiteSettingsKeys
                                         where filledAdminSettings.All(s => s.Key != settingKey)
                                         select CreateNewEmptySettingByKey(settingKey)).ToList();

            return unfilledAdminSettings;
        }

        /// <summary>Inserts the site configuration settings to the data storage. Owner will not be filled.</summary>
        /// <param name="settings">List of the site configuration settings.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        public int InsertSiteSettings(List<Setting> settings)
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

        /// <summary>Gets the site configuration setting by its key.</summary>
        /// <param name="key">Setting key.</param>
        /// <returns>Setting entity.</returns>
        public Setting GetSiteSetting(SettingKey key)
        {
            return _watchWordUnitOfWork.SettingsRepository.GetByСondition(s => s.Key == key && s.Owner == null);
        }

        /// <summary>Creates new empty setting with the specified key.</summary>
        /// <param name="key">Setting key.</param>
        /// <returns>Setting entity.</returns>
        private static Setting CreateNewEmptySettingByKey(SettingKey key)
        {
            var newSetting = new Setting { Key = key };

            var mapping = SettingKeyToTypeMapping.First(m => m.Key == key);
            newSetting.Type = mapping.Value;

            return newSetting;
        }
    }
}