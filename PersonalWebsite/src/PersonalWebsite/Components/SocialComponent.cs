using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Components
{
    public class SocialComponent : ViewComponent
    {
        private readonly ISettingModel settingModel;

        public SocialComponent(ISettingModel settingModel)
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
