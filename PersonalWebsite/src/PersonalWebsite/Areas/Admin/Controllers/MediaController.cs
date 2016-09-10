using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Areas.Admin.Controllers
{
    public class MediaController : Controller
    {
        private readonly IImageService imageService;

        public MediaController(IImageService imageService)
        {
            this.imageService = imageService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var viewModel = imageService.GetImages();
            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ImageDto model)
        {
            if(ModelState.IsValid)
            {
                imageService.AddImage(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
