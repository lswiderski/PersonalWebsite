using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite.Components
{
    public class MapOfAdventuresComponent : ViewComponent
    {
        public MapOfAdventuresComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}