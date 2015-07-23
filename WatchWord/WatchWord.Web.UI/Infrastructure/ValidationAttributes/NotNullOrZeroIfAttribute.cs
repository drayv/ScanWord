using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatchWord.Web.UI.Infrastructure.ValidationAttributes
{

    public class NotNullOrZeroIfAttribute : ValidationAttribute, IClientValidatable
    {

        private string _field;

        private string _value;

        /// <summary>
        ///Initializes a new instance of the <see cref="NotNullOrZeroIfAttribute" /> class.
        /// </summary>
        /// <param name="field">The name of the field that contains value.</param>
        /// <param name="value">The value to compare.</param>
        public NotNullOrZeroIfAttribute(string field, string value)
        {
            _field = field;
            _value = value;
        }

        /// <summary>
        /// Applies validation rules for client validation.
        /// </summary>
        /// <param name="metadata">Metadata.</param>
        /// <param name="context">The controller context</param>
        /// <returns>The list of validation rules</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();

            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("field", _field);
            rule.ValidationParameters.Add("val", _value);
            rule.ValidationType = "reqif";

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
            object requiredValue;
            try
            {
                requiredValue = validationContext.ObjectType.GetProperty(_field).GetValue(validationContext.ObjectInstance);
            }
            catch (Exception)
            {
                throw new ArgumentException("Wrong argument value: value");
            }

            if (CheckEquality(requiredValue, _value))
            {
                if (DataExist(value))
                {
                    return ValidationResult.Success;
                }

                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks equality of two values.
        /// </summary>
        /// <param name="first">The first value.</param>
        /// <param name="second">The second value.</param>
        /// <returns>The result of compare.</returns>
        private bool CheckEquality(object first, string second)
        {
            var stringValue = first.ToString();

            return StringComparer.InvariantCulture.Compare(stringValue.Trim(), second.Trim()) == 0;
        }

        /// <summary>
        /// Checks if value is not null and more then zero.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>The result of checking.</returns>
        private bool DataExist(object value)
        {
            if (value != null)
            {
                return (Int32)value > 0;
            }

            return false;
        }
    }
}