using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services;
using PersonalWebsite.Services.Models;
using Sakura.AspNetCore;
using System.Collections.Generic;
using System.Linq;

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

        [ResponseCache(Duration = 60)]
        public IActionResult Blog(string id, int? page)
        {
            if (string.IsNullOrEmpty(id))
            {
                var cacheKey = string.Format("BlogList");
                IEnumerable<SimplifiedPostViewModel> model;
                if (!_cacheService.Get(cacheKey, out model))
                {
                    model = _postModel.GetPublishedSimplifiedPosts().ToList();
                    _cacheService.Store(cacheKey, model);
                }

                int pageNumber = (page ?? 1);

                var viewModel = model.ToPagedList(pageSize, pageNumber);
                ViewData["SiteHeader"] = "Blog";
                return View(viewModel);
            }
            else
            {
                var cacheKey = string.Format("Blog-id-{0}", id);
                PostViewModel post;
                if (!_cacheService.Get(cacheKey, out post))
                {
                    post = _postModel.GetPublishedPost(id);
                    _cacheService.Store(cacheKey, post);
                }

                if (post != null)
                {
                    return View("Post", post);
                }
            }

            return NotFound(id);
        }

        public IActionResult Tag(string id, int? page)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var cacheKey = string.Format("Blog-ByTag-{0}", id);
                IEnumerable<SimplifiedPostViewModel> viewModel;
                if (!_cacheService.Get(cacheKey, out viewModel))
                {
                    viewModel = _postModel.GetPublishedSimplifiedPostsByTag(id).ToList();
                    _cacheService.Store(cacheKey, viewModel);
                }

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
                var cacheKey = string.Format("Blog-ByCategory-{0}", id);
                IEnumerable<SimplifiedPostViewModel> viewModel;
                if (!_cacheService.Get(cacheKey, out viewModel))
                {
                    viewModel = _postModel.GetPublishedSimplifiedPostsByCategory(id).ToList();
                    _cacheService.Store(cacheKey, viewModel);
                }

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

            var cacheKey = string.Format("Project-id-{0}", id);
            PostViewModel post;
            if (!_cacheService.Get(cacheKey, out post))
            {
                post = _postModel.GetPublishedPost(id);
                _cacheService.Store(cacheKey, post);
            }

            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Projects(int? page)
        {
            var cacheKey = "Projects";
            IEnumerable<SimplifiedPostViewModel> viewModel;
            if (!_cacheService.Get(cacheKey, out viewModel))
            {
                viewModel = _postModel.GetPublishedSimplifiedPostsByCategory("Project").ToList();
                _cacheService.Store(cacheKey, viewModel);
            }

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

            var cacheKey = string.Format("Adventure-id-{0}", id);
            PostViewModel post;
            if (!_cacheService.Get(cacheKey, out post))
            {
                post = _postModel.GetPublishedPost(id);
                _cacheService.Store(cacheKey, post);
            }

            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Adventures(int? page)
        {
            var cacheKey = "Adventures";
            IEnumerable<SimplifiedPostViewModel> viewModel;
            if (!_cacheService.Get(cacheKey, out viewModel))
            {
                viewModel = _postModel.GetPublishedSimplifiedPostsByCategory("Adventure").ToList();
                _cacheService.Store(cacheKey, viewModel);
            }

            int pageNumber = (page ?? 1);
            ViewBag.SiteHeader = "Category: #Adventure";
            return View(viewModel.ToPagedList(pageSize, pageNumber));
        }

        [HttpGet]
        public JsonResult AdventuresForMap()
        {
            var cacheKey = "MapOfAdventures";
            List<AdventureMapPointerDTO> viewModel;
            if (!_cacheService.Get(cacheKey, out viewModel))
            {
                viewModel = _adventureModel.GetListForMap();
                foreach (var adventure in viewModel)
                {
                    var url = Url.Action("Adventure", "Home", new { id = adventure.Url });
                    var imgUrl = string.Format("{0}://{1}/{2}", HttpContext.Request.Scheme,
                        HttpContext.Request.Host.Value, adventure.CustomData);

                    adventure.CustomData = string.Format("<img src='{0}' style='width:180px;' /><br /> <p>{1}</p>", imgUrl, adventure.Title);
                    adventure.Url = url;
                }
                _cacheService.Store(cacheKey, viewModel);
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