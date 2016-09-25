using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Components
{
    public class Socials : ViewComponent
    {
        private readonly ISettingModel settingModel;

        public Socials(ISettingModel settingModel)
        {
            this.settingModel = settingModel;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = settingModel.GetDictionary();
            return View(viewModel);
        }
    }
}
