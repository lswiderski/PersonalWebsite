using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private readonly ITagModel tagModel;

        public TagsController(ITagModel tagModel)
        {
            this.tagModel = tagModel;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Add(AddTagViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return ViewComponent("AdminAddEditTag");
            }

            tagModel.AddTag(model);

            return ViewComponent("AdminAddEditTag");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            tagModel.DeleteTag(id);
            return ViewComponent("AdminAddEditTag");
        }

        [HttpPost]
        public IActionResult GetAll()
        {
            return ViewComponent("AdminTagList");

        }

        public IActionResult Edit(int id)
        {
            var viewModel = tagModel.GetTag(id);

            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("AdminAddEditTag", new { tag = model });
            }

            tagModel.EditTag(model);

            return ViewComponent("AdminAddEditTag");
        }

        [HttpPost]
        public IActionResult GetEditTagComponent(int id)
        {
            var viewModel = tagModel.GetTagForEdit(id);
            return ViewComponent("AdminAddEditTag", new { tag = viewModel });
        }

        [HttpPost]
        public IActionResult GetAddForm()
        {
            return ViewComponent("AdminAddEditTag");
        }


    }
}
