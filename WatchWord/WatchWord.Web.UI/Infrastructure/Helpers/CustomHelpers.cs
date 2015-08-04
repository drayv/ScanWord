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

        /// <summary> Returns HTML markup with bootstap classes for the model property.</summary>
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

            var builder = new StringBuilder();
            foreach (var value in Enum.GetValues(model.GetType()))
            {
                var label = new TagBuilder("label");
                var isChecked = value.Equals(model);
                var id = TagBuilder.CreateSanitizedId(string.Format("{0}{1}", metadata.PropertyName, value));

                label.AddCssClass("btn btn-primary " + (isChecked? "active": string.Empty));
                var radio = helper.RadioButton(metadata.PropertyName, value, isChecked, new { id = id });
                label.InnerHtml = string.Format("{0} {1}", radio.ToHtmlString(), value);

                builder.Append(label.ToString());
            }
            return new MvcHtmlString(builder.ToString());
        }
    }
}