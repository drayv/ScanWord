using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using WatchWord.Web.UI.Models.Materials;
using WatchWord.Web.UI.Infrastructure.ValidationAttributes;
using System.Web;
using Moq;

namespace ScanWord.Web.UI.Tests
{
    [TestClass]
    public class HttpPostedFileRequired_Tests
    {
        public static ValidationContext GetContext()
        {
            var model = new ParseMaterialViewModel();
            return new ValidationContext(model);
        }

        public  HttpPostedFileRequiredAttribute GetInstance()
        {
            return new HttpPostedFileRequiredAttribute();
        }

        public HttpPostedFile GetFile(int contentLength)
        {
            Mock<HttpPostedFile> file = new Mock<HttpPostedFile>();
            file.Setup(n => n.ContentLength).Returns(contentLength);

            return file.Object;
        }

        [TestMethod]
        public void Check_when_file_is_null()
        {
            var instance = GetInstance();
            var ctx = GetContext();

            var result = instance.GetValidationResult(null, ctx);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Check_when_file_exists()
        {
            var instance = GetInstance();
            var ctx = GetContext();

            var result = instance.GetValidationResult(GetFile(5000), ctx);

            Assert.AreEqual(result, ValidationResult.Success);
        }
    }
}
