using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ImageProcessorCore;

namespace PersonalWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly IImageService imageService;
        private readonly IHostingEnvironment environment;


        public MediaController(IImageService imageService, IHostingEnvironment environment)
        {
            this.imageService = imageService;
            this.environment = environment;
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
        public async Task<IActionResult> Add(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), string.Format(@"Content/Uploads/{0}/", DateTime.Now.ToString("yyyy_MM")));
            Directory.CreateDirectory(uploads);
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);

                        Image image = new Image(fileStream);
                        var width = image.Bounds.Width;
                        var height = image.Bounds.Height;
                    }
                }
            }
            return View();
        }
    }
}
