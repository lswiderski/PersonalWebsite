using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Services.Models
{
    public class SettingModel : ISettingModel
    {
        private readonly IDataContext db;

        public SettingModel(IDataContext db)
        {
            this.db = db;
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

        public void DeleteSetting(int id)
        {
            var setting = db.Settings.Where(x => x.SettingId == id).FirstOrDefault();

            db.Settings.Remove(setting);
            db.SaveChanges();
        }
    }
}