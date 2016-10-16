using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using System.Collections.Generic;

namespace PersonalWebsite.Components
{
    public class LatestPostsCompoment : ViewComponent
    {
        private readonly IPostModel _postModel;

        public LatestPostsCompoment(IPostModel postModel)
        {
            _postModel = postModel;
        }

        public IViewComponentResult Invoke(int number, List<string> categories = null, string view = "Default")
        {
            var viewModel = _postModel.GetTopPublishedPosts(number, categories);
            return View(view, viewModel);
        }
    }
}