using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WatchWord.Web.UI.Infrastructure.Helpers
{
    /// <summary>The custom helpers.</summary>
    public static class CustomHelpers
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

        /// <summary>Returns HTML markup with bootstap classes for the model property.</summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <typeparam name="TValue">The property value typr</typeparam>
        /// <param name="helper">The <see cref="HtmlHelper"/>.</param>
        /// <param name="exp">The <see cref="Expression"/>.</param>
        /// <returns>HTML markup.</returns>
        public static MvcHtmlString BootstrapEnumRadioFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> exp)
        {
            var metadata = ModelMetadata.FromLambdaExpression(exp, helper.ViewData);
            var model = metadata.Model;

            if (model == null)
            {
                throw new NullReferenceException("Model");
            }

            var innerBuilder = new StringBuilder();
            foreach (var value in Enum.GetValues(model.GetType()))
            {
                var label = new TagBuilder("label");
                var isChecked = value.Equals(model);
                var id = TagBuilder.CreateSanitizedId(string.Format("{0}{1}", metadata.PropertyName, value));

                label.AddCssClass("btn btn-primary " + (isChecked ? "active" : string.Empty));

                var radio = helper.RadioButton(metadata.PropertyName, value, isChecked, new { id });
                label.InnerHtml = string.Format("{0} {1}", radio.ToHtmlString(), value);

                innerBuilder.Append(label);
            }

            return new MvcHtmlString(innerBuilder.ToString());
        }

        public static MvcHtmlString Pagination(this HtmlHelper helper, int totalPages, int currentPage, string action, string controller)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination pagination-sm");

            var builder = new StringBuilder();

            if (totalPages <= 9)
            {
                AddPages(builder, urlHelper, 1, totalPages, currentPage, action, controller);
            }
            else
            {
                if (currentPage <= 5)
                {
                    CurrentPageAtTheFirstPart(totalPages, currentPage, action, controller, urlHelper, builder);
                }
                else if (currentPage >= totalPages - 4)
                {
                    CurrentPageAtTheLastPart(totalPages, currentPage, action, controller, urlHelper, builder);
                }
                else
                {
                    CurrentPageAtTheMiddle(totalPages, currentPage, action, controller, urlHelper, builder);
                }

            }
            ul.InnerHtml = builder.ToString();
            return new MvcHtmlString(ul.ToString(TagRenderMode.Normal));
        }

        private static void CurrentPageAtTheMiddle(int totalPages, int currentPage, string action, string controller, UrlHelper urlHelper, StringBuilder builder)
        {
            AddPages(builder, urlHelper, 1, 2, currentPage, action, controller);
            AddEmptyElement(builder);
            AddPages(builder, urlHelper, currentPage - 2, currentPage + 2, currentPage, action, controller);
            AddEmptyElement(builder);
            AddPages(builder, urlHelper, totalPages - 1, totalPages, currentPage, action, controller);
        }

        private static void CurrentPageAtTheLastPart(int totalPages, int currentPage, string action, string controller, UrlHelper urlHelper, StringBuilder builder)
        {
            AddPages(builder, urlHelper, 1, 2, currentPage, action, controller);
            AddEmptyElement(builder);
            AddPages(builder, urlHelper, totalPages - 6, totalPages, currentPage, action, controller);
        }

        private static void CurrentPageAtTheFirstPart(int totalPages, int currentPage, string action, string controller, UrlHelper urlHelper, StringBuilder builder)
        {
            AddPages(builder, urlHelper, 1, 7, currentPage, action, controller);
            AddEmptyElement(builder);
            AddPages(builder, urlHelper, totalPages - 1, totalPages, currentPage, action, controller);
        }

        private static void AddPages(StringBuilder builder, UrlHelper urlHelper, int from, int to, int currentPage, string action, string controller)
        {
            TagBuilder li = null;
            TagBuilder a = null;

            for (int i = from; i <= to; i++)
            {
                li = new TagBuilder("li");
                if (i == currentPage)
                {
                    li.AddCssClass("active");
                }
                a = new TagBuilder("a");
                a.MergeAttribute("href", urlHelper.Action(action, controller, new { pageNumber = i }));
                a.SetInnerText(i.ToString());

                li.InnerHtml = a.ToString(TagRenderMode.Normal);
                builder.Append(li.ToString(TagRenderMode.Normal));
            }
        }

        private static void AddEmptyElement(StringBuilder builder)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("disabled");

            var a = new TagBuilder("a");
            a.MergeAttribute("href", "#");
            a.SetInnerText("...");

            li.InnerHtml = a.ToString(TagRenderMode.Normal);
            builder.Append(li.ToString());
        }
    }
}