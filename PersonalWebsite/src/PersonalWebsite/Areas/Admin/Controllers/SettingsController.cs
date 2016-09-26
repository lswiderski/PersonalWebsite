using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalWebsite.Common;
using PersonalWebsite.Controllers;

namespace PersonalWebsite.Areas.Admin.Controllers
{

    [Area("Admin")]
    
    public class SettingsController : PersonalController
    {
        private readonly ISettingModel _settingModel;
        private readonly ICacheService _cacheService;

        public SettingsController(ISettingModel settingModel, ICacheService cacheService)
        {
            _settingModel = settingModel;
            _cacheService = cacheService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var viewModel = _settingModel.GetSettings();
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
