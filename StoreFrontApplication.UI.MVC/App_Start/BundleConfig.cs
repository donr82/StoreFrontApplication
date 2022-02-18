using System.Web.Optimization;

namespace StoreFrontApplication.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

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

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Scripts/js/jquery-3.3.1.min.js",
                "~/Scripts/js/bootstrap.min.js",
                "~/Scripts/js/jquery.nice-select.min.js",
                "~/Scripts/js/jquery.nicescroll.min.js",
                "~/Scripts/js/jquery.magnific-popup.min.js",
                "~/Scripts/js/jquery.countdown.min.js",
                "~/Scripts/js/jquery.slicknav.js",
                "~/Scripts/js/mixitup.min.js",
                "~/Scripts/js/owl.carousel.min.js",
                
                "~/Scripts/js/main.js"));
                
        }
    }
}
