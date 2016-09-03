using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryModel categoryModel;

        public CategoryController(ICategoryModel categoryModel)
        {
            this.categoryModel = categoryModel;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("AdminAddEditCategory");
            }

            categoryModel.AddCategory(model);

            return ViewComponent("AdminAddEditCategory");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            categoryModel.DeleteCategory(id);
            return ViewComponent("AdminAddEditCategory");
        }

        [HttpPost]
        public IActionResult GetAll()
        {
            return ViewComponent("AdminCategoryList");
        }

        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("AdminAddEditCategory", new { category = model });
            }

            categoryModel.EditCategory(model);

            return ViewComponent("AdminAddEditCategory");
        }

        [HttpPost]
        public IActionResult GetEditCategoryComponent(int id)
        {
            var viewModel = categoryModel.GetCategoryForEdit(id);
            return ViewComponent("AdminAddEditCategory", new { category = viewModel });
        }

        [HttpPost]
        public IActionResult GetAddForm()
        {
            return ViewComponent("AdminAddEditCategory");
        }
    }
}