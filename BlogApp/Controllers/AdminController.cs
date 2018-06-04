using BlogApp.Data;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        public AdminController(IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _unitOfWork = unitOfWork;
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

        [HttpPost]
        public ActionResult AddPost(Post post)
        {
            string json;

            if (ModelState.IsValid)
            {
                // Add record to the database
                var id = _postRepository.AddPost(post);

                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Post added successfully."
                });

                // Save changes to the database
                _unitOfWork.Complete();
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the post."
                });
            }

            return Content(json, "application/json");
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