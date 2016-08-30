using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class TagModel : ITagModel
    {

        private readonly IDataContext db;

        public TagModel(IDataContext db)
        {
            this.db = db;
        }

        public void AddTag(AddTagViewModel model)
        {
            db.Tags.Add(new Tag
            {
                Name = model.Name
            });
            db.SaveChanges();
        }

        public TagViewModel GetTag(int id)
        {
            var tag = (from t in db.Tags
                            where t.TagId == id
                            select new TagViewModel
                            {
                                Name = t.Name,
                                Uses = t.Uses,
                                TagId = t.TagId
                            }).FirstOrDefault();

            return tag;
        }

        public List<TagViewModel> GetTags()
        {
            var tag = (from t in db.Tags
                              select new TagViewModel
                              {
                                  Name = t.Name,
                                  Uses = t.Uses,
                                  TagId = t.TagId
                              }).ToList();

            return tag;
        }

        public void EditTag(EditTagViewModel model)
        {
            var tag = db.Tags.SingleOrDefault(x => x.TagId == model.TagId);

            if (tag != null)
            {
                tag.Name = model.Name;
                db.SaveChanges();
            }
        }
        public int GetUsesOfTag(int id)
        {
            var uses = db.Tags.Where(x => x.TagId == id).Select(x => x.Uses).FirstOrDefault();

            return uses;
        }
        public void CalculateUses(int id)
        {
            var tag = db.Tags.SingleOrDefault(x => x.TagId == id);
            if (tag == null)
            {
                return;
            }
            var uses = tag.PostTags.Where(y => y.TagId == id).Count();

            tag.Uses = uses;
            db.SaveChanges();

        }

        public List<TagViewModel> GetTagsUsedByPost(int id)
        {
            //TODO I'm not sure if I need use condition here, check it later
            var categories = db.Posts.SingleOrDefault(x => x.PostId == id)?.PostTags.Select(y => new TagViewModel
            {
                Name = y.Tag.Name,
                TagId = y.Tag.TagId,
                Uses = y.Tag.Uses
            }).ToList();

            return categories;
        }

        public void DeleteTag(int id)
        {
            var tag = db.Tags.SingleOrDefault(x => x.TagId == id);

            if (tag != null)
            {
                db.Tags.Remove(tag);
                db.SaveChanges();
            }
        }
    }
}
