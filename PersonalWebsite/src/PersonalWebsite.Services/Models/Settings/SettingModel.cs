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
            var tags = db.Tags.ToList();
            var posts = db.Posts.ToList();
            var posts1 = posts[0].PostTags;
            var postTags = db.Posts.Where(x => x.PostId == 5).Select(y => y.PostTags).ToList();

            var t = (from post in db.Posts
                     join pt in db.PostTags on post.PostId equals pt.PostId
                     join tag in db.Tags on pt.TagId equals tag.TagId
                     where post.PostId == 5
                     select tag).ToList();

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