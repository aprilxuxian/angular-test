using System.Web.Optimization;

namespace iasset.FlightDashBoard 
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularApp").IncludeDirectory(
                   "~/Scripts/apps", "*.js").IncludeDirectory(
                   "~/Scripts/routes", "*.js").IncludeDirectory(
                   "~/Scripts/services", "*.js").IncludeDirectory(
                   "~/Scripts/controllers", "*.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));
        }
    }
}
