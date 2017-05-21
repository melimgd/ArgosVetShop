using System.Web;
using System.Web.Optimization;

namespace ArgosVetShop
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

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                    "~/Content/js/materialize.js",
                    "~/Content/js/owl.carousel.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/pagescripts").Include(
                    "~/Scripts/scripts.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/confirmation").Include(
                    "~/Scripts/confirmationscripts.js"
                ));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/css/materialize.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/owl.theme.default.min.css",
                      "~/Content/site.css"));
        }
    }
}
