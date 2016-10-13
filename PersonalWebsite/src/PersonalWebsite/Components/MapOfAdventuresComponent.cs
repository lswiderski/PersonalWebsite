using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Components
{
    public class MapOfAdventuresComponent : ViewComponent
    {
        public MapOfAdventuresComponent(IPostModel postModel)
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
