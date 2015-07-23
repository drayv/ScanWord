using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatchWord.Web.UI.Infrastructure.ValidationAttributes
{
    public class MaxFileSizeAttribute: ValidationAttribute, IClientValidatable
    {
        private int _maxSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxFileSizeAttribute" /> class.
        /// </summary>
        public MaxFileSizeAttribute(int maxSize): base("{0} has to big size.")
        {
            _maxSize = maxSize;
        }

        /// <summary>
        /// Applies validation rules for client validation.
        /// </summary>
        /// <param name="metadata">Metadata.</param>
        /// <param name="context">The controller context</param>
        /// <returns>The list of validation rules</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = "maxfilesize"
            };
            rule.ValidationParameters.Add("value", _maxSize);

            yield return rule;
        }

        /// <summary>
        /// Validating the property.
        /// </summary>
        /// <param name="value">The property value</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation result.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as HttpPostedFileBase;

            if (file != null)
            {
                if (file.ContentLength > _maxSize)
                {
                    var message = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(message);
                }
            }
            return ValidationResult.Success;
        }
    }
}