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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPost(Post post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                _unitOfWork.Posts.Edit(post);

                json = JsonConvert.SerializeObject(new
                {
                    id = post.ID,
                    success = true,
                    message = "Changes saved successfully."
                });

                _unitOfWork.Save();
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            _unitOfWork.Posts.Delete(id);

            var json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Post deleted successfully."
            });

            _unitOfWork.Save();

            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult AddCategory([Bind(Exclude = "ID")]Category category)
        {
            string json;

            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Insert(category);

                json = JsonConvert.SerializeObject(new
                {
                    succes = true,
                    message = "Category added successfully."
                });

                _unitOfWork.Save();
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "Failed to add the category."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            string json;

            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Update(category);

                json = JsonConvert.SerializeObject(new
                {
                    id = category.ID,
                    success = true,
                    message = "Changes saved successfully."
                });

                _unitOfWork.Save();
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            _unitOfWork.Categories.Delete(id);

            var json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Category deleted successfully."
            });

            _unitOfWork.Save();

            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult AddTag([Bind(Exclude = "ID")]Tag tag)
        {
            string json;

            if (ModelState.IsValid)
            {
                _unitOfWork.Tags.Insert(tag);

                json = JsonConvert.SerializeObject(new
                {
                    success = true,
                    message = "Tag added successfullu."
                });

                _unitOfWork.Save();
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "Failed to add the tag."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult EditTag(Tag tag)
        {
            string json;

            if (ModelState.IsValid)
            {
                _unitOfWork.Tags.Update(tag);

                json = JsonConvert.SerializeObject(new
                {
                    id = tag.ID,
                    success = true,
                    message = "Changes saved successfully."
                });

                _unitOfWork.Save();
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes"
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult DeleteTag(int id)
        {
            _unitOfWork.Tags.Delete(id);

            var json = JsonConvert.SerializeObject(new
            {
                id = 0,
                success = true,
                message = "Tag deleted successfully."
            });

            _unitOfWork.Save();

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

        public ActionResult Categories()
        {
            var categories = _unitOfWork.Categories.GetCategories();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = _unitOfWork.Categories.TotalCategories(),
                rows = categories,
                total = 1
            }), "application/json");
        }

        public ActionResult Tags()
        {
            var tags = _unitOfWork.Tags.GetTags();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = _unitOfWork.Tags.TotalTags(),
                rows = tags,
                total = 1
            }), "application/json");
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}