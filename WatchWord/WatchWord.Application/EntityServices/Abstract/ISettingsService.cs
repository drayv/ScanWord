using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    public interface ISettingsService
    {
        /// <summary>Gets the site configuration settings, which is not yet filled.</summary>
        /// <returns>List of site configuration settings.</returns>
        List<Setting> GetUnfilledSiteSettings();

        /// <summary>Inserts the site configuration setting to the data storage. Owner will not be filled.</summary>
        /// <param name="settings">List of the site configuration settings.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        int InsertSiteSettings(List<Setting> settings);

        /// <summary>Gets the site configuration setting by his key.</summary>
        /// <param name="key">Setting key.</param>
        /// <returns>Setting entity.</returns>
        Setting GetSiteSetting(SettingKey key);
    }
}