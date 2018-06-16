using System.Web.Optimization;

namespace BlogApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            // jquery library bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery", "https://code.jquery.com/jquery-3.3.1.min.js")
                .Include("~/Scripts/jquery-{version}.js"));

            // jquery validation library
            bundles.Add(new ScriptBundle("~/bundles/jqueryval", "https://cdn.jsdelivr.net/npm/jquery-validation@1.17.0/dist/jquery.validate.min.js")
                .Include("~/Scripts/jquery.validate*"));

            // jqueryui library bundle
            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                .Include("~/Scripts/jquery-ui-{version}.js"));

            // jqueryui styles bundle
            bundles.Add(new StyleBundle("~/Content/themes/base/css")
                .Include("~/Content/themes/base/jquery-ui.css"));

            // jqgrid library bundle
            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                "~/Scripts/free-jqGrid/jquery.jqgrid.min.js",
                "~/Scripts/free-jqGrid/i18n/min/grid.locale-en.js"));

            // jqgrid styles bundle
            bundles.Add(new StyleBundle("~/Content/jqgridcss").Include(
                "~/Content/ui.jqgrid.css"));

            // tinyMCE library bundle
            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                "~/Scripts/tinymce/tinymce.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js"));

            // app script bundle
            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/Scripts/app.js"));

            // admin script bundle
            bundles.Add(new ScriptBundle("~/bundles/adminjs")
                .Include("~/Scripts/admin.js"));

            // admin styles bundle
            bundles.Add(new StyleBundle("~/Content/admincss")
                .Include("~/Content/admin.css"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}
