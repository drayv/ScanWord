using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    public interface ISettingsService
    {
        List<Setting> GetUnfilledAdminSettings();

        int InsertAdminSettings(List<Setting> settings);

        Setting GetAdminSetting(SettingKey key);
    }
}