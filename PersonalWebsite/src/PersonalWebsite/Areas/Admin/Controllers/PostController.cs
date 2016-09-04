﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostModel postModel;

        public PostController(IPostModel postModel)
        {
            this.postModel = postModel;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var viewModel = postModel.GetSimplifiedPosts();
            return View(viewModel);
        }

        public IActionResult Add()
        {
            
            return View();
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