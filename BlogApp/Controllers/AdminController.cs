using BlogApp.Models;
using BlogApp.Service;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    [RequireHttps]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IPostRepository _postRepository;

        public AdminController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [Route("Admin/Manage")]
        public ActionResult Manage()
        {
            return View();
        }

        [Route("Admin/Posts")]
        public ActionResult Posts(JqInViewModel jqParams)
        {
            var posts = _postRepository.GetAllPosts(jqParams.page, jqParams.rows, jqParams.sidx, jqParams.sord == "asc");

            var totalPosts = _postRepository.TotalPosts(false);

            // Return posts, count and other information in the
            // JSON format needed by the jqGrid
            return Content(JsonConvert.SerializeObject(new
            {
                page = jqParams.page,
                records = totalPosts,
                rows = posts,
                total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)
            }, new CustomDateTimeConverter()), "application/json");
        }
    }
}