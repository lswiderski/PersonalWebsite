using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Services.Models
{
    public class TagModel : ITagModel
    {
        private readonly DataContext db;

        public TagModel(DataContext db)
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

        public EditTagViewModel GetTagForEdit(int id)
        {
            var tag = GetTag(id);
            if (tag != null)
            {
                return new EditTagViewModel
                {
                    Name = tag.Name,
                    TagId = tag.TagId
                };
            }
            return new EditTagViewModel();
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
            var tags = db.Posts.SingleOrDefault(x => x.PostId == id)?.PostTags.Select(y => new TagViewModel
            {
                Name = y.Tag.Name,
                TagId = y.Tag.TagId,
                Uses = y.Tag.Uses
            }).ToList();

            return tags;
        }

        public void DeleteTag(int id)
        {
            var tag = db.Tags.SingleOrDefault(x => x.TagId == id);
            var postTags = db.PostTags.Where(x => x.TagId == id);

            if (tag != null)
            {
                db.Tags.Remove(tag);
                db.PostTags.RemoveRange(postTags);
                db.SaveChanges();
            }
        }
    }
}