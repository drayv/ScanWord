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
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap/bootstrap.css", "~/Content/Site.css"));
            // libraries
            bundles.Add(new ScriptBundle("~/Scripts/jquery+bootstrap").Include("~/Scripts/jquery-{version}.js", "~/Scripts/bootstrap.js", "~/Scripts/respond.js"));
            // validation
            bundles.Add(new ScriptBundle("~/Scripts/validation").Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/customValidation.js"));
        }
    }
}