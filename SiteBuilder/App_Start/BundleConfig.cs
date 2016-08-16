using System.Web;
using System.Web.Optimization;

namespace SiteBuilder
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/showpagesite").Include(
                        "~/Scripts/showPageSite.js"));

            bundles.Add(new ScriptBundle("~/bundles/dragndrop").Include(
                        "~/Scripts/DragNDrop.js"));

            bundles.Add(new ScriptBundle("~/bundles/loadstarttemplate").Include(
                        "~/Scripts/loadStartTemplate.js"));

            bundles.Add(new ScriptBundle("~/bundles/loadtemplate").Include(
                        "~/Scripts/loadTemplate.js"));

            bundles.Add(new ScriptBundle("~/bundles/cloudinaryall").Include(
                        "~/Scripts/cloudinary_all.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-markdown").Include(
                      "~/Scripts/bootstrap-markdown.js",
                      "~/Scripts/markdown.js",
                      "~/Scripts/marked.js",
                      "~/Scripts/to-markdown.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-markdown").Include(
                      "~/Content/bootstrap-markdown.min.css"));
        }
    }
}
