using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WatchWord.Web.UI.Infrastructure.Helpers
{
    /// <summary>The account helpers.</summary>
    public static class AccountHelpers
    {
        /// <summary>Returns HTML markup with bootstrap classes for form validation.</summary>
        /// <param name="helper">HTML helper.</param>
        /// <param name="excludePropertyErrors">Errors for exclude.</param>
        /// <returns>HTML markup.</returns>
        public static MvcHtmlString BootstrapValidationSummaryFor(this HtmlHelper helper, bool excludePropertyErrors)
        {
            if (helper.ViewData.ModelState.IsValid)
            {
                return new MvcHtmlString(string.Empty);
            }

            var alert = new TagBuilder("div");
            alert.AddCssClass("alert alert-danger");
            alert.InnerHtml = helper.ValidationSummary(excludePropertyErrors).ToHtmlString();
            return new MvcHtmlString(alert.ToString(TagRenderMode.Normal));
        }
    }
}