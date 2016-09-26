using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using Microsoft.AspNetCore.Authorization;
using PersonalWebsite.Controllers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : PersonalController
    {

        public HomeController()
        {
            
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
