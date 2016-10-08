using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Areas.Admin.Components
{
    public class SelectImageComponent : ViewComponent
    {
        private readonly ISettingModel settingModel;
        private readonly IImageService imageService;
        private int pageSize = 10;

        public SelectImageComponent(ISettingModel settingModel, IImageService imageService)
        {
            this.settingModel = settingModel;
            this.imageService = imageService;
        }

        public IViewComponentResult Invoke(string modalTarget, string name = null, string inputIdForId = null, string inputIdForURL = null, string inputIdForThumbnailURL = null)
        {
            var model = imageService.GetImageToSelectViewModels(string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host.Value));
            int pageNumber = 1;
            ViewData["name"] = name;
            ViewData["modalTarget"] = modalTarget;
            ViewData["inputId"] = inputIdForId;
            ViewData["inputIdForURL"] = inputIdForURL;
            ViewData["inputIdForThumbnailURL"] = inputIdForThumbnailURL;
            var viewModel = model.ToPagedList(pageSize, pageNumber);

            return View(viewModel);
        }
    }
}
