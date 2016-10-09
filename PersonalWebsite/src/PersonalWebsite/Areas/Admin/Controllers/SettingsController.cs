using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalWebsite.Controllers;
using PersonalWebsite.Services;
using Sakura.AspNetCore;

namespace PersonalWebsite.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class SettingsController : PersonalController
    {
        private readonly ISettingModel _settingModel;
        private readonly ICacheService _cacheService;
        private int pageSize = 10;

        public SettingsController(ISettingModel settingModel, ICacheService cacheService)
        {
            _settingModel = settingModel;
            _cacheService = cacheService;
        }

        // GET: /<controller>/
        public IActionResult Index(int? page)
        {
            var model = _settingModel.GetSettings();

            int pageNumber = (page ?? 1);
            var viewModel = model.ToPagedList(pageSize, pageNumber);
            ViewData["SiteHeader"] = "Blog";
            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                _settingModel.AddSetting(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = _settingModel.GetSettingForEdit(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                _settingModel.EditSetting(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult ClearCache()
        {
            _cacheService.Clear();
            return RedirectToAction("Index");
        }
    }
}
