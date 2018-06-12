using BlogApp.Data;
using System.Web.Mvc;

namespace BlogApp
{
    public static class Extensions
    {
        public static string Href(this Post post, UrlHelper helper)
        {
            return helper.RouteUrl(
                new { action = "Index", controller = "Post", year = post.PostedOn.Year, month = post.PostedOn.Month, titleSlug = post.UrlSlug });
        }
    }
}