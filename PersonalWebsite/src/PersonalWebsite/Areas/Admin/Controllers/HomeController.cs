using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        ISettingModel settingModel;
        public HomeController(ISettingModel settingModel)
        {
            this.settingModel = settingModel;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var viewModel = settingModel.GetSettings();
            return View(viewModel);
        }
    }
}
