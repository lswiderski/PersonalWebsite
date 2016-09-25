using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using PersonalWebsite.Common;

namespace PersonalWebsite.Services.Models
{
    public class SettingModel : ISettingModel
    {
        private readonly DataContext db;
        private readonly ICacheService _cacheService;

        public SettingModel(DataContext db, ICacheService cacheService)
        {
            this.db = db;
            _cacheService = cacheService;
        }

        public void AddSetting(AddSettingViewModel model)
        {
            db.Settings.Add(new Setting
            {
                Name = model.Name,
                Type = model.Type,
                Value = model.Value
            });
            db.SaveChanges();
        }

        public void EditSetting(EditSettingViewModel model)
        {
            var setting = db.Settings.Where(x => x.SettingId == model.SettingId).FirstOrDefault();

            setting.Name = model.Name;
            setting.Type = model.Type;
            setting.Value = model.Value;

            db.SaveChanges();
        }

        public EditSettingViewModel GetSettingForEdit(int id)
        {
            var setting = db.Settings.Where(x => x.SettingId == id)
                .Select(x => new EditSettingViewModel
                {
                    SettingId = x.SettingId,
                    Name = x.Name,
                    Value = x.Value,
                    Type = x.Type
                }
                ).FirstOrDefault();

            return setting;
        }

        public SettingViewModel GetSetting(int id)
        {
            var setting = db.Settings.Where(x => x.SettingId == id)
                .Select(x => new SettingViewModel
                {
                    SettingId = x.SettingId,
                    Name = x.Name,
                    Value = x.Value,
                    Type = x.Type
                }
                ).FirstOrDefault();

            return setting;
        }

        public List<SettingViewModel> GetSettings()
        {
            var settings = db.Settings.Select(x => new SettingViewModel
            {
                SettingId = x.SettingId,
                Name = x.Name,
                Value = x.Value,
                Type = x.Type
            }).ToList();

            return settings;
        }

        public Dictionary<string, SettingViewModel> GetDictionary()
        {
           var key = "Settings";
           var cachedSettings = _cacheService.Get<Dictionary<string, SettingViewModel>>(key);
            if (cachedSettings == null)
            {
                var settings = db.Settings.Select(x => new SettingViewModel
                {
                    SettingId = x.SettingId,
                    Name = x.Name,
                    Value = x.Value,
                    Type = x.Type
                }).ToDictionary(x => x.Name, x => x);

                _cacheService.Store(key, settings);
                return settings;
            }
            return cachedSettings;

        }

        public void DeleteSetting(int id)
        {
            var setting = db.Settings.Where(x => x.SettingId == id).FirstOrDefault();

            db.Settings.Remove(setting);
            db.SaveChanges();
        }
    }
}