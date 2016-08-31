using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Areas.Admin.Components
{
    public class AdminAddEditTag : ViewComponent
    {
        public IViewComponentResult Invoke(EditTagViewModel tag)
        {
            if(tag != null)
            {
                return View("Edit",tag);
            }
            else
            {
                return View("Add");
            }
            
        }
    }
}