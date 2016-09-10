using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;
using Sakura.AspNetCore;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostModel postModel;
        private int pageSize = 3;

        public HomeController(IPostModel postModel)
        {
            this.postModel = postModel;
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

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
                var model = postModel.GetPublishedSimplifiedPosts();
                
                int pageNumber = (page ?? 1);
                var viewModel = model.ToPagedList(pageSize, pageNumber);
                ViewData["SiteHeader"] = "Blog";
                return View(viewModel);
            }

            var post = postModel.GetPublishedPost(id);
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
                var viewModel = postModel.GetPublishedSimplifiedPostsByTag(id);
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
                var viewModel = postModel.GetPublishedSimplifiedPostsByCategory(id);
                int pageNumber = (page ?? 1);
                ViewBag.SiteHeader = string.Format("Category: #{0}", id);
                return View("Blog", viewModel.ToPagedList(pageSize, pageNumber));
            }

            return NotFound(id);
        }

        public IActionResult Search(string id, int? page)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = postModel.Search(id);
                int pageNumber = (page ?? 1);
                return View("Blog", viewModel.ToPagedList(pageSize, pageNumber));
            }

            return NotFound(id);
        }

        public IActionResult Project(string id, int? page)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Projects", new { page = page });
            }

            var post = postModel.GetPublishedPost(id);
            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Projects(int? page)
        {
            var viewModel = postModel.GetPublishedSimplifiedPostsByCategory("Project");
            int pageNumber = (page ?? 1);
            ViewBag.SiteHeader = "Category: #Project";
            return View("Blog", viewModel.ToPagedList(pageSize, pageNumber));
        }

        public IActionResult Adventure(string id, int? page)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Adventures", new { page = page });
            }

            var post = postModel.GetPublishedPost(id);
            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Adventures(int? page)
        {
            var viewModel = postModel.GetPublishedSimplifiedPostsByCategory("Adventure");
            int pageNumber = (page ?? 1);
            ViewBag.SiteHeader = "Category: #Adventure";
            return View("Blog", viewModel.ToPagedList(pageSize, pageNumber));
        }
    }
}