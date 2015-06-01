namespace WatchWord.Web.UI
{
    using System.Web.Optimization;

    /// <summary>
    /// The bundle config.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Register bundles.
        /// </summary>
        /// <param name="bundles">
        /// The bundles.
        /// </param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/bootstrap-theme.css", "~/Content/Site.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include("~/Scripts/jquery-{version}.js", "~/Scripts/bootstrap.js"));
        }
    }
}