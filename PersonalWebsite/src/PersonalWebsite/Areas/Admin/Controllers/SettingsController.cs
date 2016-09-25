using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Areas.Admin.Controllers
{

    [Area("Admin")]
    
    public class SettingsController : Controller
    {
        ISettingModel settingModel;
        public SettingsController(ISettingModel settingModel)
        {
            this.settingModel = settingModel;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var viewModel = settingModel.GetSettings();
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
                settingModel.AddSetting(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = settingModel.GetSettingForEdit(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                settingModel.EditSetting(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
