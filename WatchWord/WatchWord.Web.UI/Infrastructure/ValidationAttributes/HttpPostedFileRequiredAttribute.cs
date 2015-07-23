using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace WatchWord.Web.UI.Infrastructure.ValidationAttributes
{
    public class HttpPostedFileRequiredAttribute: ValidationAttribute, IClientValidatable
    {
        /// <summary>Applies validation rules for client validation.</summary>
        /// <param name="metadata">Metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>The list of validation rules.</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = "filerequired",
            };

            yield return rule;
        }

        /// <summary>Validates the property.</summary>
        /// <param name="value">The property value</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation result.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as HttpPostedFileBase;
            if (file != null && file.ContentLength != 0) return ValidationResult.Success;

            var message = this.FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(message);
        }
    }
}