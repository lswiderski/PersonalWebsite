using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Controllers;
using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using PersonalWebsite.Services.Models;
using Sakura.AspNetCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class PostController : PersonalController
    {
        private readonly IPostModel postModel;
        private readonly ICategoryModel categoryModel;
        private int pageSize = 10;

        public PostController(IPostModel postModel, ICategoryModel categoryModel)
        {
            this.postModel = postModel;
            this.categoryModel = categoryModel;
            var x = new Post();
        }
        // GET: /<controller>/
        public IActionResult Index(int? page)
        {
            var model = postModel.GetSimplifiedPosts();

            int pageNumber = (page ?? 1);
            var viewModel = model.ToPagedList(pageSize, pageNumber);
            ViewData["SiteHeader"] = "Blog";
            return View(viewModel);;
        }

        public IActionResult Add()
        {
            AddPostViewModel viewModel = new AddPostViewModel();

            viewModel.Categories = categoryModel.GetEmptyCategoriesCheckBoxList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddPostViewModel model)
        {
            if(ModelState.IsValid)
            {
                postModel.AddPost(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var viewModel = postModel.GetPostForEdit(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditPostViewModel model)
        {
            if(ModelState.IsValid)
            {
                postModel.UpdatePost(model);
                return RedirectToAction("Index", "Post", new { area = "Admin", id = "" });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            postModel.DeletePost(id);

            return RedirectToAction("Index", "Post", new { area = "Admin", id = "" });
            
        }
    }
}
