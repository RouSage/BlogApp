using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Repo;
using BlogApp.Service;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    [RequireHttps]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        [ValidateInput(false)]
        [Route("Admin/AddPost")]
        public ActionResult AddPost(Post post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                // Add record to the database
                _unitOfWork.Posts.AddPost(post);

                json = JsonConvert.SerializeObject(new
                {
                    //id = id,
                    success = true,
                    message = "Post added successfully."
                });

                // Save changes to the database
                _unitOfWork.Save();
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    //id = 0,
                    success = false,
                    message = "Failed to add the post."
                });
            }

            return Content(json, "application/json");
        }

        public ActionResult GetCategoriesHtml()
        {
            var categories = _unitOfWork.Categories.GetCategories();

            var sb = new StringBuilder();
            sb.AppendLine("<select>");

            foreach (var category in categories)
            {
                sb.AppendFormat(@"<option value=""{0}"">{1}</option>",
                    category.ID, category.Name).AppendLine();
            }

            sb.AppendLine("</select>");

            return Content(sb.ToString(), "text/html");
        }

        public ActionResult GetTagsHtml()
        {
            var tags = _unitOfWork.Tags.GetTags();

            var sb = new StringBuilder();
            sb.AppendLine(@"<select multiple=""multiple"">");

            foreach (var tag in tags)
            {
                sb.AppendFormat(@"<option value=""{0}"">{1}</option>",
                    tag.ID, tag.Name).AppendLine();
            }

            sb.AppendLine("</select>");

            return Content(sb.ToString(), "text/html");
        }

        [Route("Admin/Posts")]
        public ActionResult Posts(JqInViewModel jqParams)
        {
            var posts = _unitOfWork.Posts.GetAllPosts(jqParams.page, jqParams.rows, jqParams.sidx, jqParams.sord == "asc");

            var totalPosts = _unitOfWork.Posts.TotalPosts(false);

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

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}