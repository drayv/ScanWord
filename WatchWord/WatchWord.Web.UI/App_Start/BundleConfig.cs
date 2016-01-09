using System.Web.Optimization;

namespace WatchWord.Web.UI
{
    /// <summary>The bundle config.</summary>
    public class BundleConfig
    {
        /// <summary>Register bundles.</summary>
        /// <param name="bundles">The bundle collection.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/theme.css",
                "~/Content/jquery.contextMenu.css",
                "~/Content/site.css"));

            // libraries
            bundles.Add(new ScriptBundle("~/Scripts/libraries").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.contextMenu.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/google.analytics.js"));

            // validation
            bundles.Add(new ScriptBundle("~/Scripts/validation").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/customValidation.js"));
        }
    }
}