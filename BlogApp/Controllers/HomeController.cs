using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Service;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _pageSize = 5;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int page = 1)
        {
            PostViewModel model = new PostViewModel
            {
                Posts = _unitOfWork.Posts.GetPublishedPosts(page, _pageSize),
                PageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = _pageSize,
                    TotalItems = _unitOfWork.Posts.TotalPosts()
                }
            };

            return View(model);
        }

        [Route("Acrhive/{category}")]
        public ActionResult Category(string category, int page = 1)
        {
            var model = new PostViewModel
            {
                Posts = _unitOfWork.Posts.GetPublishedPostsByCategory(category, page, _pageSize),
                PageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = _pageSize,
                    TotalItems = _unitOfWork.Posts.TotalPublishedPostsForCategory(category)
                }
            };

            return View("Index", model);
        }

        [Route("Archive/{tag}")]
        public ActionResult Tag(string tag, int page = 1)
        {
            var model = new PostViewModel
            {
                Posts = _unitOfWork.Posts.GetPublishedPostsByTag(tag, page, _pageSize),
                PageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = _pageSize,
                    TotalItems = _unitOfWork.Posts.TotalPublishedPostsForTag(tag)
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

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(
                        ConfigurationManager.AppSettings["mailAccount"],
                        ConfigurationManager.AppSettings["mailPassword"]),
                    EnableSsl = true
                };

                var adminMail = ConfigurationManager.AppSettings["mailAccount"];
                var from = new MailAddress(contact.Email, "BlogApp Messenger - " + contact.Name);
                var to = new MailAddress(adminMail, "BlogApp Admin");

                using (var message = new MailMessage(from, to)
                {
                    IsBodyHtml = true,
                    Body = contact.Body,
                    BodyEncoding = Encoding.UTF8,

                    Subject = contact.Subject,
                    SubjectEncoding = Encoding.UTF8,

                    ReplyTo = new MailAddress(contact.Email)
                })

                client.Send(message);

                // Add message to the database
                _unitOfWork.ContactRepository.Insert(contact);
                _unitOfWork.Save();

                return View("Thanks");
            }

            return View();
        }

        [Route("Feed")]
        public ActionResult Feed()
        {
            var blogTitle = ConfigurationManager.AppSettings["BlogTitle"];
            var blogDescription = ConfigurationManager.AppSettings["BlogDescription"];
            var blogUrl = ConfigurationManager.AppSettings["BlogUrl"];

            var posts = _unitOfWork.Posts.GetPublishedPosts(1, 25);

            // Create a collection of SyndicationItems from the latest posts
            List<SyndicationItem> collection = new List<SyndicationItem>();

            foreach (var post in posts)
            {
                collection.Add(new SyndicationItem(post.Title, post.Content, new System.Uri(string.Concat(blogUrl, post.Href(Url)))));
            }

            // Create an instance if SyndicationFeed class passing the SyndicationItem collection
            var feed = new SyndicationFeed(blogTitle, blogDescription, new System.Uri(blogUrl), collection)
            {
                Copyright = new TextSyndicationContent(string.Format("Copyright (c) {0}", blogTitle)),
                Language = "en-US"
            };

            // Format feed in RSS format
            var feedFormatter = new Rss20FeedFormatter(feed);

            // Call the custom action that write the feed to the response
            return new FeedResult(feedFormatter);
        }

        [ChildActionOnly]
        public ActionResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel
            {
                Categories = _unitOfWork.Categories.GetCategories(),
                Tags = _unitOfWork.Tags.GetTags()
            };

            foreach (var category in widgetViewModel.Categories)
            {
                category.Frequence = _unitOfWork.Posts.TotalPublishedPostsForCategory(category.UrlSlug);
            }

            return PartialView("_Sidebars", widgetViewModel);
        }
    }
}