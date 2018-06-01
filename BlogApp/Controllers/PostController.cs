using BlogApp.Models;
using BlogApp.Service;
using System.Web.Mvc;
using BlogApp.Data;
using System.Collections.Generic;
using System.Web;

namespace BlogApp.Controllers
{
    [RequireHttps]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly int _pageSize = 5;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: Post
        [Route("Archive/{year}/{month}/{titleSlug}")]
        public ActionResult Index(int year, int month, string titleSlug)
        {
            var post = _postRepository.GetPost(year, month, titleSlug);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (!post.Published)
                throw new HttpException(401, "THe post is not published");

            return View(post);
        }
    }
}