using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface IPostModel
    {
        IQueryable<SimplifiedPostViewModel> GetPublishedSimplifiedPosts();
        void AddPost(AddPostViewModel model);
        PostViewModel GetPublishedPost(int id);
        PostViewModel GetPublishedPost(string name);
        EditPostViewModel GetPostForEdit(int id);
        void UpdatePost(EditPostViewModel model);
        void DeletePost(int id);
        IQueryable<SimplifiedPostViewModel> GetSimplifiedPosts();
        IQueryable<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByTag(string tagName);
        IQueryable<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByCategory(string categoryName);
        IQueryable<SimplifiedPostViewModel> Search(string query);
        List<SimplifiedPostViewModel> GetPublishedSimplifiedPostsForFeed(int count);
        List<SimplifiedPostViewModel> GetTopPublishedPosts(int number, List<string> categories);
        void AddNewDraft(EditPostViewModel model);
    }
}
