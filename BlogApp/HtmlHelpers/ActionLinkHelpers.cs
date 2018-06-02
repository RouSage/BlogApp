using BlogApp.Data;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BlogApp.HtmlHelpers
{
    public static class ActionLinkHelpers
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post, string htmlClass)
        {
            return helper.ActionLink(post.Title, "Index", "Post",
                new { year = post.PostedOn.Year, month = post.PostedOn.Month, titleSlug = post.UrlSlug },
                new { @class = htmlClass });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category, string htmlClass)
        {
            return helper.ActionLink(category.Name, "Category", "Home", new { category = category.UrlSlug },
                new { @class = htmlClass, title = string.Format("See all posts in {0}", category.Name) });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag, string htmlClass)
        {
            return helper.ActionLink(tag.Name, "Tag", "Home", new { tag = tag.UrlSlug },
                new { @class = htmlClass, title = string.Format("See all posts in {0}", tag.Name) });
        }
    }
}