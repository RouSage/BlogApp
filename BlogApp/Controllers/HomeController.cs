using BlogApp.Models;
using BlogApp.Service;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly int _pageSize = 5;

        public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        public ActionResult Index(int page = 1)
        {
            PostViewModel model = new PostViewModel
            {
                Posts = _postRepository.GetPublishedPosts(page, _pageSize),
                PageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = _pageSize,
                    TotalItems = _postRepository.TotalPosts()
                }
            };

            return View(model);
        }

        [Route("Acrhive/{category}")]
        public ActionResult Category(string category, int page = 1)
        {
            var model = new PostViewModel
            {
                Posts = _postRepository.GetPublishedPostsByCategory(category, page, _pageSize),
                PageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = _pageSize,
                    TotalItems = _postRepository.TotalPublishedPostsForCategory(category)
                }
            };

            return View("Index", model);
        }

        [Route("Archive/{tag}")]
        public ActionResult Tag(string tag, int page = 1)
        {
            var model = new PostViewModel
            {
                Posts = _postRepository.GetPublishedPostsByTag(tag, page, _pageSize),
                PageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = _pageSize,
                    TotalItems = _postRepository.TotalPublishedPostsForTag(tag)
                }
            };

            return View("Index", model);
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

        [ChildActionOnly]
        public ActionResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel
            {
                Categories = _categoryRepository.GetCategories(),
                Tags = _tagRepository.GetTags()
            };

            foreach (var category in widgetViewModel.Categories)
            {
                category.Frequence = _postRepository.TotalPublishedPostsForCategory(category.UrlSlug);
            }

            return PartialView("_Sidebars", widgetViewModel);
        }
    }
}