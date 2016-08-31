using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Areas.Admin.Components
{
    public class AdminTagList : ViewComponent
    {
        private readonly ITagModel tagModel;

            public AdminTagList(ITagModel tagModel)
        {
            this.tagModel = tagModel;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = tagModel.GetTags();
            return View(viewModel);
        }
    }
}
