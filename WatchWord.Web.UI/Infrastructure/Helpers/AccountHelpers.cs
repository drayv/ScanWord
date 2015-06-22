using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WatchWord.Web.UI.Infrastructure.Helpers
{
    public static class AccountHelpers
    {
        /// <summary>
        /// Returns HTML markup with bootstrap classes for form validation. 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="excludePropertyErrors"></param>
        /// <returns>HTML markup</returns>
        public static MvcHtmlString BootstrapValidationSummaryFor(this HtmlHelper helper, bool excludePropertyErrors)
        {
            if (!helper.ViewData.ModelState.IsValid)
            {
                TagBuilder alert = new TagBuilder("div");
                alert.AddCssClass("alert alert-danger");
                alert.InnerHtml = helper.ValidationSummary(excludePropertyErrors).ToHtmlString();
                return new MvcHtmlString(alert.ToString(TagRenderMode.Normal));
            }
            else
            {
                return new MvcHtmlString("");
            }
        }
    }
}