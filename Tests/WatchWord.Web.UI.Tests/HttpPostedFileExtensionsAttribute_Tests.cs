﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using WatchWord.Web.UI.Models.Materials;
using WatchWord.Web.UI.Infrastructure.ValidationAttributes;
using System.Web;
using Moq;
using System.Linq;
using System.Web.ModelBinding;

namespace ScanWord.Web.UI.Tests
{
    [TestClass]
    public class HttpPostedFileExtensionsAttribute_Tests
    {
        public static ValidationContext GetContext()
        {
            var model = new ParseMaterialViewModel();
            return new ValidationContext(model);
        }

        public static HttpPostedFileExtensionsAttribute getInstance(string extensions)
        {
            return new HttpPostedFileExtensionsAttribute() { Extensions = extensions };
        }

        public static HttpPostedFileBase GetFile(string fileName)
        {
            Mock<HttpPostedFileBase> moq = new Mock<HttpPostedFileBase>();
            moq.Setup(n => n.FileName).Returns(fileName);
            return moq.Object;
        }

        [TestMethod]
        public void Check_valid_extension_from_single_string()
        {
            var ctx = GetContext();
            var file = GetFile("somefile.txt");
            var instance = getInstance(".txt");

            var result = instance.GetValidationResult(file, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        public void Check_valid_extension_from_duo_string()
        {
            var ctx = GetContext();
            var file = GetFile("somefile.txt");
            var instance = getInstance(".txt, .ass");

            var result = instance.GetValidationResult(file, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        public void Check_invalid_extension()
        {
            var ctx = GetContext();
            var file = GetFile("somefile.jpg");
            var instance = getInstance(".txt, .ass");

            var result = instance.GetValidationResult(file, ctx);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Check_if_file_does_not_exist()
        {
            var ctx = GetContext();
            var instance = getInstance(".txt, .ass");

            var result = instance.GetValidationResult(null, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }
    }
}
