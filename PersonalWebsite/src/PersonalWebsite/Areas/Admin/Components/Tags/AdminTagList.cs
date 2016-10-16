using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Areas.Admin.Components
{
    public class AdminTagList : ViewComponent
    {
        private readonly ITagModel tagModel;

        public AdminTagList(ITagModel tagModel)
        {
            this.tagModel = tagModel;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = tagModel.GetTags();
            return View(viewModel);
        }
    }
}