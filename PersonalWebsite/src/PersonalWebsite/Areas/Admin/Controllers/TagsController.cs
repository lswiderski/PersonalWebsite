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
                return PartialView(model);
            }

            tagModel.AddTag(model);

            return PartialView();
        }
        
        public IActionResult GetAll()
        {
            var viewModel = tagModel.GetTags();

            return PartialView(viewModel);

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
                return PartialView(model);
            }

            tagModel.EditTag(model);

            return PartialView();
        }


        
    }
}
