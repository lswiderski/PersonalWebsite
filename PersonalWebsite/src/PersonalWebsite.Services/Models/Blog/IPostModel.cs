using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface IPostModel
    {
        List<SimplifiedPostViewModel> GetPublishedSimplifiedPosts();
        void AddPost(AddPostViewModel model);
        PostViewModel GetPublishedPost(int id);
        PostViewModel GetPublishedPost(string name);
        EditPostViewModel GetPostForEdit(int id);
        void UpdatePost(EditPostViewModel model);
        void DeletePost(int id);
        List<SimplifiedPostViewModel> GetSimplifiedPosts();
        List<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByTag(string tagName);
        List<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByCategory(string categoryName);
        List<SimplifiedPostViewModel> Search(string query);
    }
}
