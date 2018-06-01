using BlogApp.Data;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BlogApp.HtmlHelpers
{
    public static class ActionLinkHelpers
    {
        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category, string htmlClass)
        {
            return helper.ActionLink(category.Name, "Category", "Home", new { category = category.UrlSlug },
                new { @class = htmlClass, title = string.Format("See all posts in {0}", category.Name) });
        }
    }
}