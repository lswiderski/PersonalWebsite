using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite.Components
{
    public class SidebarComponent : ViewComponent
    {
        public SidebarComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}