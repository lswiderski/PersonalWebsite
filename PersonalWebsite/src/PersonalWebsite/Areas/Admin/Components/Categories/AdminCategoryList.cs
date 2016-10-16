using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Areas.Admin.Components
{
    public class AdminCategoryList : ViewComponent
    {
        private readonly ICategoryModel categoryModel;

        public AdminCategoryList(ICategoryModel categoryModel)
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