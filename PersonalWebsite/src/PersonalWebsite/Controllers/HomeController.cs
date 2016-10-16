using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services;
using PersonalWebsite.Services.Models;
using Sakura.AspNetCore;

namespace PersonalWebsite.Controllers
{
    public class HomeController : PersonalController
    {
        private readonly IPostModel _postModel;
        private readonly ISettingModel _settingModel;
        private readonly ICacheService _cacheService;
        private readonly IXmlFeedService _xmlFeedService;
        private readonly IAdventureModel _adventureModel;
        private int pageSize = 3;

        public HomeController(IPostModel postModel, ISettingModel settingModel, ICacheService cacheService, IXmlFeedService xmlFeedService, IAdventureModel adventureModel)
        {
            _postModel = postModel;
            _settingModel = settingModel;
            _cacheService = cacheService;
            _xmlFeedService = xmlFeedService;
            _adventureModel = adventureModel;
            pageSize = _settingModel.GetInt("Blog.PageSize");
        }

        public IActionResult Index()
        {
            Logger.Info("Entered to Index");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Blog(string id, int? page)
        {
            if (string.IsNullOrEmpty(id))
            {
                var model = _postModel.GetPublishedSimplifiedPosts();

                int pageNumber = (page ?? 1);
                var viewModel = model.ToPagedList(pageSize, pageNumber);
                ViewData["SiteHeader"] = "Blog";
                return View(viewModel);
            }

            var post = _postModel.GetPublishedPost(id);
            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Tag(string id, int? page)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = _postModel.GetPublishedSimplifiedPostsByTag(id);
                int pageNumber = (page ?? 1);
                ViewBag.SiteHeader = string.Format("Tag: #{0}", id);
                return View("Blog", viewModel.ToPagedList(pageSize, pageNumber));
            }

            return NotFound(id);
        }

        public IActionResult Category(string id, int? page)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = _postModel.GetPublishedSimplifiedPostsByCategory(id);
                int pageNumber = (page ?? 1);
                ViewBag.SiteHeader = string.Format("Category: #{0}", id);
                return View("Blog", viewModel.ToPagedList(pageSize, pageNumber));
            }

            return NotFound(id);
        }

        public IActionResult Search(string query, int? page)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var viewModel = _postModel.Search(query);
                int pageNumber = (page ?? 1);
                ViewBag.SiteHeader = string.Format("Search: #{0}", query);
                ViewBag.Query = query;
                return View(viewModel.ToPagedList(pageSize, pageNumber));
            }

            return RedirectToAction("index");
        }

        public IActionResult Project(string id, int? page)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Projects", new { page });
            }

            var post = _postModel.GetPublishedPost(id);
            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Projects(int? page)
        {
            var viewModel = _postModel.GetPublishedSimplifiedPostsByCategory("Project");
            int pageNumber = (page ?? 1);
            ViewBag.SiteHeader = "Category: #Project";
            return View(viewModel.ToPagedList(pageSize, pageNumber));
        }

        public IActionResult Adventure(string id, int? page)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Adventures", new { page });
            }

            var post = _postModel.GetPublishedPost(id);
            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Adventures(int? page)
        {
            var viewModel = _postModel.GetPublishedSimplifiedPostsByCategory("Adventure");
            int pageNumber = (page ?? 1);
            ViewBag.SiteHeader = "Category: #Adventure";
            return View(viewModel.ToPagedList(pageSize, pageNumber));
        }

        [HttpGet]
        public JsonResult AdventuresForMap()
        {
            var viewModel = _adventureModel.GetListForMap();

            foreach (var adventure in viewModel)
            {
                var url = Url.Action("Adventure", "Home", new { id = adventure.Url });
                var imgUrl = string.Format("{0}://{1}/{2}", HttpContext.Request.Scheme,
                    HttpContext.Request.Host.Value, adventure.CustomData);

                adventure.CustomData = string.Format("<img src='{0}' style='width:180px;' /><br /> <p>{1}</p>", imgUrl, adventure.Title);
                adventure.Url = url;
            }
            return Json(viewModel);
        }

        public IActionResult RSS()
        {
            var x = Request;
            var feed = _xmlFeedService.BuildXmlFeed(this.ControllerContext);
            return Content(feed, "application/rss+xml");
        }

        public IActionResult Feed()
        {
            var feed = _xmlFeedService.BuildXmlFeed(this.ControllerContext);
            return Content(feed, "application/rss+xml");
        }
    }
}