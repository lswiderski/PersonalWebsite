using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PersonalWebsite.Common;
using PersonalWebsite.Common.Models;
using PersonalWebsite.Services;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Components
{
    public class TwitterComponent : ViewComponent
    {
        private readonly ISettingModel settingModel;

        private Logger _logger = LogManager.GetCurrentClassLogger();

        public TwitterComponent(ISettingModel settingModel, ICacheService cacheService)
        {
            this.settingModel = settingModel;

        }

        public IViewComponentResult Invoke()
        {

            var twitter = new TwitterDTO
            {
                TwitterUserName = settingModel.GetString("Twitter.UserName"),
                TwitterWidgetId = settingModel.GetString("Twitter.Widget.ID")
            };

            return View(twitter);
        }
    }
}
