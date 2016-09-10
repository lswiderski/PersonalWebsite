using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
