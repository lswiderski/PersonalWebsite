using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostModel postModel;

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

        public IActionResult Blog(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var viewModel = postModel.GetPublishedSimplifiedPosts();
                return View(viewModel);
            }

            var post = postModel.GetPublishedPost(id);
            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Tag(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = postModel.GetPublishedSimplifiedPostsByTag(id);
                return View("Blog", viewModel);
            }

            return NotFound(id);
        }

        public IActionResult Category(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = postModel.GetPublishedSimplifiedPostsByCategory(id);
                return View("Blog", viewModel);
            }

            return NotFound(id);
        }

        public IActionResult Search(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = postModel.Search(id);
                return View("Blog", viewModel);
            }

            return NotFound(id);
        }

        public IActionResult Project(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = postModel.GetPublishedSimplifiedPostsByCategory("Project");
                return View("Blog", viewModel);
            }

            var post = postModel.GetPublishedPost(id);
            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Projects()
        {
            var viewModel = postModel.GetPublishedSimplifiedPostsByCategory("Project");
            return View("Blog", viewModel);
        }

        public IActionResult Adventure(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var viewModel = postModel.GetPublishedSimplifiedPostsByCategory("Adventure");
                return View("Blog", viewModel);
            }

            var post = postModel.GetPublishedPost(id);
            if (post != null)
            {
                return View("Post", post);
            }

            return NotFound(id);
        }

        public IActionResult Adventures(string id)
        {
            var viewModel = postModel.GetPublishedSimplifiedPostsByCategory("Adventure");
            return View("Blog", viewModel);
        }
    }
}