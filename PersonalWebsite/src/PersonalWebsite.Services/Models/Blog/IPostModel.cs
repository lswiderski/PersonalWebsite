using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface IPostModel
    {
        IEnumerable<SimplifiedPostViewModel> GetPublishedSimplifiedPosts();
        void AddPost(AddPostViewModel model);
        PostViewModel GetPublishedPost(int id);
        PostViewModel GetPublishedPost(string name);
        EditPostViewModel GetPostForEdit(int id);
        void UpdatePost(EditPostViewModel model);
        void DeletePost(int id);
        IEnumerable<SimplifiedPostViewModel> GetSimplifiedPosts();
        IEnumerable<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByTag(string tagName);
        IEnumerable<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByCategory(string categoryName);
        IEnumerable<SimplifiedPostViewModel> Search(string query);
    }
}
