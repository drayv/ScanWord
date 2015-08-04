using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace WatchWord.Web.UI.Infrastructure.ValidationAttributes
{
    public class HttpPostedFileExtensionsAttribute : DataTypeAttribute, IClientValidatable
    {
        private readonly FileExtensionsAttribute _innerAttribute =
            new FileExtensionsAttribute();

        /// <summary>Initializes a new instance of the <see cref="HttpPostedFileExtensionsAttribute" /> class.</summary>
        public HttpPostedFileExtensionsAttribute()
            : base(DataType.Upload)
        {
            ErrorMessage = _innerAttribute.ErrorMessage;
        }

        /// <summary>Gets or sets the file name extensions.</summary>
        /// <returns>The file name extensions, or the default file extensions (".png", ".jpg", ".jpeg", and ".gif") if the property is not set.</returns>
        public string Extensions
        {
            get { return _innerAttribute.Extensions; }
            set { _innerAttribute.Extensions = value; }
        }

        /// <summary>Applies validation rules for client validation.</summary>
        /// <param name="metadata">Metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>The list of validation ri=ukes.</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "extension",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            rule.ValidationParameters["extensions"] = _innerAttribute.Extensions;
            yield return rule;
        }

        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <returns>The formatted error message.</returns>
        /// <param name="name">The name of the field that caused the validation failure.</param>
        public override string FormatErrorMessage(string name)
        {
            return _innerAttribute.FormatErrorMessage(name);
        }

        /// <summary>Checks that the specified file name extension or extensions is valid.</summary>
        /// <returns>True if the file name extension is valid; otherwise, false.
        /// </returns>
        /// <param name="value">A comma delimited list of valid file extensions.</param>
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            return _innerAttribute.IsValid(file != null ? file.FileName : value);
        }
    }
}