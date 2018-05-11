using BlogApp.Models;
using BlogApp.Service;
using System.Web.Mvc;
using BlogApp.Data;
using System.Collections.Generic;

namespace BlogApp.Controllers
{
    [RequireHttps]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: Post
        public ActionResult Index()
        {
            ViewBag.Title = "Latest Posts";

            return View();
        }
    }
}