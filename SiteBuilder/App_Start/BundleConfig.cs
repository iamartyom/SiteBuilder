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

            bundles.Add(new ScriptBundle("~/bundles/tags").Include(
                        "~/Scripts/tag-it.js",
                        "~/Scripts/tags.js"));

            bundles.Add(new ScriptBundle("~/bundles/showdown").Include(
                        "~/Scripts/showdown.js"));

            bundles.Add(new ScriptBundle("~/bundles/editsitepanel").Include(
                        "~/Scripts/editSitePanel.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-touchspin").Include(
                        "~/Scripts/touchspin.js",
                        "~/Scripts/jquery.bootstrap-touchspin.js"));

            bundles.Add(new ScriptBundle("~/bundles/updatecontent").Include(
                        "~/Scripts/updateContent.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-white").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-black").Include(
                      "~/Content/bootstrap-black.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-ginger").Include(
                      "~/Content/bootstrap-ginger.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/site.css"));            

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-markdown").Include(
                      "~/Scripts/bootstrap-markdown.js",
                      "~/Scripts/markdown.js",
                      "~/Scripts/marked.js",
                      "~/Scripts/to-markdown.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-markdown").Include(
                      "~/Content/bootstrap-markdown.min.css"));

            bundles.Add(new StyleBundle("~/Content/buttons").Include(
                      "~/Content/buttons.css"));

            bundles.Add(new StyleBundle("~/Content/tags").Include(
                      "~/Content/tagit.css",
                      "~/Content/tagit-ui.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-touchspin").Include(
                      "~/Content/jquery.bootstrap-touchspin.css"));
        }
    }
}
