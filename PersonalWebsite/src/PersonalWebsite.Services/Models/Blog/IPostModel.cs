using PersonalWebsite.Common.Enums;
using System.Collections.Generic;
using System.Linq;

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

        IQueryable<SimplifiedPostViewModel> GetSimplifiedPosts();

        IEnumerable<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByTag(string tagName);

        IEnumerable<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByCategory(string categoryName);

        IQueryable<SimplifiedPostViewModel> Search(string query);

        List<SimplifiedPostViewModel> GetPublishedSimplifiedPostsForFeed(int count);

        List<SimplifiedPostViewModel> GetTopPublishedPosts(int number, List<string> categories);

        void AddNewDraft(EditPostViewModel model);

        void SetStatus(List<int> ids, PostStatusType status);
    }
}