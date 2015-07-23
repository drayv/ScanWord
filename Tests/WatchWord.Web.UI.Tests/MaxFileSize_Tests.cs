using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using WatchWord.Web.UI.Infrastructure.ValidationAttributes;
using WatchWord.Web.UI.Models.Materials;

namespace ScanWord.Web.UI.Tests
{
    [TestClass]
    public class MaxFileSizeTests
    {
        public static HttpPostedFileBase GetFile(int fileSize)
        {
            Mock<HttpPostedFileBase> moq = new Mock<HttpPostedFileBase>();
            moq.Setup(n => n.ContentLength).Returns(fileSize);

            return moq.Object;
        }

        public static MaxFileSizeAttribute GetInstance(int maxFileSize)
        {
            return new MaxFileSizeAttribute(maxFileSize);
        }

        public static ValidationContext GetContext()
        {
            return new ValidationContext(new ParseMaterialViewModel());
        }

        [TestMethod]
        public void Check_when_file_has_maximum_valid_size()
        {
            var instance = GetInstance(5999);
            var file = GetFile(5999);
            var ctx = GetContext();

            var result = instance.GetValidationResult(file, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }

        [TestMethod]
        public void Check_when_invalid_file_size()
        {
            var instance = GetInstance(5999);
            var file = GetFile(6000);
            var ctx = GetContext();

            var result = instance.GetValidationResult(file, ctx);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Check_when_file_does_not_exist()
        {
            var instance = GetInstance(5999);
            var ctx = GetContext();

            var result = instance.GetValidationResult(null, ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }
    }
}
