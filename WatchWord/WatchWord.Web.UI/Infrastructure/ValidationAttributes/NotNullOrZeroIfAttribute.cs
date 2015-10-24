using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WatchWord.Web.UI.Infrastructure.ValidationAttributes
{
    public class NotNullOrZeroIfAttribute : ValidationAttribute, IClientValidatable
    {

        private readonly string _field;
        private readonly string _value;

        /// <summary>Initializes a new instance of the <see cref="NotNullOrZeroIfAttribute" /> class.</summary>
        /// <param name="field">The name of the field that contains value.</param>
        /// <param name="value">The value to compare.</param>
        public NotNullOrZeroIfAttribute(string field, string value)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            if (value == null)
                throw new ArgumentNullException("value");

            _field = field;
            _value = value;
        }

        /// <summary>Applies validation rules for client validation.</summary>
        /// <param name="metadata">Metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>The list of validation rules.</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule { ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()) };

            rule.ValidationParameters.Add("field", _field);
            rule.ValidationParameters.Add("val", _value);
            rule.ValidationType = "reqif";

            yield return rule;
        }

        /// <summary>Validating the property.</summary>
        /// <param name="value">The property value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation result.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_field);
            if (property == null)
                throw new ValidationException("Invalid property name");

            var propertValue = property.GetValue(validationContext.ObjectInstance);

            if (!(string.Equals(propertValue == null? null : propertValue.ToString(), _value, StringComparison.InvariantCulture)))
                return ValidationResult.Success;

            var intValue = value as int?;
            if (intValue.HasValue && intValue.Value > 0)
                return ValidationResult.Success;

            var errorMessage = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(errorMessage);
        }
    }
}