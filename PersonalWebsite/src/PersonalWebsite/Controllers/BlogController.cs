using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Services.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalWebsite.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostModel postModel;

        public BlogController(IPostModel postModel)
        {
            this.postModel = postModel;
        }
        // GET: /<controller>/
        public IActionResult Index(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                var viewModel = postModel.GetPublishedSimplifiedPosts();
                return View(viewModel);
            }
            
                var post = postModel.GetPublishedPost(id);
                if(post != null)
                {
                    return View("Post", post);
                }

            return NotFound(id);



        }
    }
}
