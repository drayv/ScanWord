using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchWord.Web.UI.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using WatchWord.Web.UI.Models.Materials;
using WatchWord.Domain.Entity;

namespace ScanWord.Web.UI.Tests
{
    [TestClass]
    public class NotNullOrZeroIfTests
    {
        public static ValidationContext GetContext(MaterialType value)
        {
            return new ValidationContext(GetModel(value));
        }

        public static ParseMaterialViewModel GetModel(MaterialType value)
        {
            return new ParseMaterialViewModel { Type = value };
        }

        public static NotNullOrZeroIfAttribute GetInstance(string fieldName, string fielValue)
        {
            return new NotNullOrZeroIfAttribute(fieldName, fielValue);
        }

        [TestMethod]
        public void Invalid_if_null()
        {
            var ctx = GetContext(MaterialType.Series);
            var instance = GetInstance("Type", "Series");

            var result = instance.GetValidationResult(null, ctx);

            Assert.IsNotNull(result, "Invalid result type");
        }

        [TestMethod]
        public void Check_when_type_is_valid_and_value_is_zero()
        {
            var ctx = GetContext(MaterialType.Series);
            var instance = GetInstance("Type", "Series");

            var result = instance.GetValidationResult(0, ctx);

            Assert.IsNotNull(result, "Invalid result type");
        }

        [TestMethod]
        public void Check_when_type_is_valid_and_value_is_null()
        {
            var ctx = GetContext(MaterialType.Film);
            var instance = GetInstance("Type", "Series");

            var result = instance.GetValidationResult(null, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        public void Check_when_type_is_invalid_and_value_is_zero()
        {
            var ctx = GetContext(MaterialType.Film);
            var instance = GetInstance("Type", "Series");

            var result = instance.GetValidationResult(0, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        public void Check_when_type_is_invalid_and_value_is_null()
        {
            var ctx = GetContext(MaterialType.Film);
            var instance = GetInstance("Type", "Series");

            var result = instance.GetValidationResult(null, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        public void Check_when_type_is_valid_and_value_is_bigger_then_zero()
        {
            var ctx = GetContext(MaterialType.Series);
            var instance = GetInstance("Type", "Series");

            var result = instance.GetValidationResult(5, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Check_when_propertyName_is_invalid()
        {
            var ctx = GetContext(MaterialType.Series);
            var instance = GetInstance("Typ", "Series");

            var result = instance.GetValidationResult(5, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }
    }
}
