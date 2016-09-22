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
            var viewModel = imageService.GetImageViewModels(string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host.Value));
            
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
            var addedIds = new List<int>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var newFileName = file.FileName.Replace(' ', '_');
                    var path = Path.Combine(uploads, newFileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        var pathToSave = Path.Combine(string.Format(@"Media/{0}/", DateTime.Now.ToString("yyyy_MM")), newFileName);
                        Image image = new Image(fileStream);
                        var width = image.Bounds.Width;
                        var height = image.Bounds.Height;
                        
                        UploadImageDto img = new UploadImageDto
                        {
                            Width = width,
                            Height = height,
                            Path = pathToSave,
                            Type = file.ContentType,
                            Length = file.Length,
                            Name = newFileName,
                            NameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName),
                            Extension = Path.GetExtension(path),
                            UploadedOn = DateTime.Now
                        };
                        addedIds.Add(imageService.AddImage(img));
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult ResizeImage(int id, float scale = 1)
        {
            imageService.ResizeImage(id, scale);

            return RedirectToAction("Index");
        }

        public IActionResult UploadedList(List<int> ids)
        {
            var viewModel = imageService.GetImageViewModels(ids,string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host.Value));

            return PartialView(viewModel);
        }
    }
}
