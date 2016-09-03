using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Areas.Admin.Components
{ 
    public class AdminAddEditCategory : ViewComponent
    {
        public IViewComponentResult Invoke(EditCategoryViewModel category)
        {
            if (category != null)
            {
                return View("Edit", category);
            }
            else
            {
                return View("Add");
            }

        }
    }
}
