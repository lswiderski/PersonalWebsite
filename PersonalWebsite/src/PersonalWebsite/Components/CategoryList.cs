using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services;
using PersonalWebsite.Services.Models;
using System.Collections.Generic;

namespace PersonalWebsite.Components
{
    public class CategoryList : ViewComponent
    {
        private readonly ICategoryModel _categoryModel;
        private readonly ICacheService _cacheService;

        public CategoryList(ICategoryModel categoryModel, ICacheService cacheService)
        {
            _categoryModel = categoryModel;
            _cacheService = cacheService;
        }

        public IViewComponentResult Invoke()
        {
            var cacheKey = "CategoryListComponent";
            List<CategoryViewModel> viewModel;
            if (!_cacheService.Get(cacheKey, out viewModel))
            {
                viewModel = _categoryModel.GetCategories();
                _cacheService.Store(cacheKey, viewModel);
            }

            return View(viewModel);
        }
    }
}