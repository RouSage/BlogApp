using BlogApp.Models;
using BlogApp.Service;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;

        public HomeController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public ActionResult Index(int page = 1)
        {
            int pageSize = 5;

            PostViewModel model = new PostViewModel
            {
                Posts = _postRepository.GetPosts(page, pageSize),
                PageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = _postRepository.TotalPosts()
                }
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.selectedItem = "about";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.selectedItem = "contact";

            return View();
        }
    }
}