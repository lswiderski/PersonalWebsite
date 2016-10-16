using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Components
{
    public class CategoryList : ViewComponent
    {
        private readonly ICategoryModel categoryModel;

        public CategoryList(ICategoryModel categoryModel)
        {
            this.categoryModel = categoryModel;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = categoryModel.GetCategories();
            return View(viewModel);
        }
    }
}