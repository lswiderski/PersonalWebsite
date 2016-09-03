using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
