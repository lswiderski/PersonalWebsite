using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface ISettingModel
    {
        void AddSetting(AddSettingViewModel model);

        void EditSetting(EditSettingViewModel model);

        EditSettingViewModel GetSettingForEdit(int id);

        SettingViewModel GetSetting(int id);

        IQueryable<SettingViewModel> GetSettings();

        Dictionary<string, SettingViewModel> GetDictionary();

        decimal GetDecimal(string key);

        bool GetLogic(string key);

        int GetInt(string key);

        string GetString(string key);
    }
}
