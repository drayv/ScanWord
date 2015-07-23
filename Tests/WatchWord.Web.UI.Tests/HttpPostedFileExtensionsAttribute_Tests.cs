using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using WatchWord.Web.UI.Models.Materials;
using WatchWord.Web.UI.Infrastructure.ValidationAttributes;
using System.Web;
using Moq;

namespace ScanWord.Web.UI.Tests
{
    [TestClass]
    public class HttpPostedFileExtensionsAttributeTests
    {
        public static ValidationContext GetContext()
        {
            var model = new ParseMaterialViewModel();
            return new ValidationContext(model);
        }

        public static HttpPostedFileExtensionsAttribute GetInstance(string extensions)
        {
            return new HttpPostedFileExtensionsAttribute() { Extensions = extensions };
        }

        public static HttpPostedFileBase GetFile(string fileName)
        {
            var moq = new Mock<HttpPostedFileBase>();
            moq.Setup(n => n.FileName).Returns(fileName);
            return moq.Object;
        }

        [TestMethod]
        public void Check_valid_extension_from_single_string()
        {
            var ctx = GetContext();
            var file = GetFile("somefile.txt");
            var instance = GetInstance(".txt");

            var result = instance.GetValidationResult(file, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        public void Check_valid_extension_from_duo_string()
        {
            var ctx = GetContext();
            var file = GetFile("somefile.txt");
            var instance = GetInstance(".txt, .ass");

            var result = instance.GetValidationResult(file, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        public void Check_invalid_extension()
        {
            var ctx = GetContext();
            var file = GetFile("somefile.jpg");
            var instance = GetInstance(".txt, .ass");

            var result = instance.GetValidationResult(file, ctx);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Check_if_file_does_not_exist()
        {
            var ctx = GetContext();
            var instance = GetInstance(".txt, .ass");

            var result = instance.GetValidationResult(null, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }
    }
}
