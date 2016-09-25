using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Components
{
    public class Tags : ViewComponent
    {
        private readonly ITagModel _tagModel;

        public Tags(ITagModel tagModel)
        {
            _tagModel = tagModel;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = _tagModel.GetTags();
            return View(viewModel);
        }
    }
}
