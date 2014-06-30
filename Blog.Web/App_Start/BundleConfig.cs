using System.Web.Optimization;

namespace Blog.Web
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/entry-pagination").Include(
                "~/Scripts/custom/entry-pagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/user-settings").Include(
                "~/Scripts/custom/user-settings.js"));

            bundles.Add(new ScriptBundle("~/bundles/blog-settings").Include(
                "~/Scripts/custom/blog-settings.js"));

            bundles.Add(new ScriptBundle("~/bundles/blogentry").Include(
                "~/Scripts/custom/blogentry.js"));

            bundles.Add(new ScriptBundle("~/bundles/categories").Include(
                "~/Scripts/custom/categories.js"));

            bundles.Add(new ScriptBundle("~/bundles/general").Include(
                "~/Scripts/bootstrap/*.js",
                "~/Scripts/bootstrap/extras/*.js",
                "~/Scripts/bootstrap/theme/*.js",
//                "~/Scripts/bootstrap/bootstrap.min.js",
//                "~/Scripts/bootstrap/modern-business.js",
//                "~/Scripts/bootstrap/bootstrap/extras/*.js",
//                "~/Scripts/bootstrap/bootstrap/theme/*.js",
//                "~/Scripts/bootstrap/bootstrap-switch.min.js",
//                "~/Scripts/bootstrap/bootstrap-switch.js",
                "~/Scripts/custom/general.js",
                "~/Scripts/jquery/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.unobtrusive*",
                        "~/Scripts/jquery/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery/jquery.validate*"));






//            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
//                        "~/Scripts/jquery-{version}.js",
//                        "~/Scripts/modern-business.js",
//                        "~/Scripts/bootstrap.js",
//                        "~/Scripts/bootstrap.min.js",
//                        "~/Scripts/bootstrap-switch.min.js",
//                        "~/Scripts/bootstrap-switch.js"));
//
//            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
//                        "~/Scripts/Site.js"));

//            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
//                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap.min.css",
                "~/font-awesome/css/font-awesome.css",
                "~/font-awesome/css/font-awesome.min.css",
                "~/Content/bootstrap/bootstrap-glyphicons.css",
                "~/Content/bootstrap-switch.css",
                "~/Content/bootstrap-switch.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}