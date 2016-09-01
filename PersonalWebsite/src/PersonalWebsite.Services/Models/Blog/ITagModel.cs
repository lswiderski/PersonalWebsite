using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface ITagModel
    {
        void AddTag(AddTagViewModel model);

        void DeleteTag(int id);
        TagViewModel GetTag(int id);
        List<TagViewModel> GetTags();
        void EditTag(EditTagViewModel model);
        int GetUsesOfTag(int id);
        void CalculateUses(int id);
        List<TagViewModel> GetTagsUsedByPost(int id);

        EditTagViewModel GetTagForEdit(int id);
    }
}
