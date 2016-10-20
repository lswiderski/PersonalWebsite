using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services;
using PersonalWebsite.Services.Models;
using System.Collections.Generic;

namespace PersonalWebsite.Components
{
    public class Tags : ViewComponent
    {
        private readonly ITagModel _tagModel;
        private readonly ICacheService _cacheService;

        public Tags(ITagModel tagModel, ICacheService cahCacheService)
        {
            _tagModel = tagModel;
            _cacheService = cahCacheService;
        }

        public IViewComponentResult Invoke()
        {
            var cacheKey = "TagsListComponent";
            List<TagViewModel> viewModel;
            if (!_cacheService.Get(cacheKey, out viewModel))
            {
                viewModel = _tagModel.GetTags();
                _cacheService.Store(cacheKey, viewModel);
            }

            return View(viewModel);
        }
    }
}