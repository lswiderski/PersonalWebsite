using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

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