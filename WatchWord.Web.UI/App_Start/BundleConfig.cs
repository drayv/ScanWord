using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WatchWord.Web.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/bootstrap-theme.css", "~/Content/Site.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include("~/Scripts/jquery-{version}.js", "~/Scripts/bootstrap.js"));
        }
    }
}