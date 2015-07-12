using System.Web.Optimization;

namespace WatchWord.Web.UI
{
    /// <summary>
    /// The bundle config.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Register bundles.
        /// </summary>
        /// <param name="bundles">
        /// The bundle collection.
        /// </param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/bootstrap-theme.css", "~/Content/Site.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include("~/Scripts/jquery-{version}.js", "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/Scripts/validation").Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/customValidation.js"));
        }
    }
}