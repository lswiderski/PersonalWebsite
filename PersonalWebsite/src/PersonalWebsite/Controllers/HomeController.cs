using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using Sakura.AspNetCore;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostModel _postModel;
        private readonly ISettingModel _settingModel;
        private int pageSize = 3;

        public HomeController(IPostModel postModel, ISettingModel settingModel)
        {
            _postModel = postModel;
            _settingModel = settingModel;
            pageSize = _settingModel.GetInt("Blog.PageSize");
        }

        public IActionResult Index()
        {
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
            return View("Blog", viewModel.ToPagedList(pageSize, pageNumber));
        }

        public IActionResult Adventure(string id, int? page)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Adventures", new {  page });
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
            return View("Blog", viewModel.ToPagedList(pageSize, pageNumber));
        }
    }
}