using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchWord.Domain.Entity;
using WatchWord.Web.UI.Models.Materials;

namespace ScanWord.Web.UI.Tests
{
    [TestClass]
    public class DisplayAllViewModel_Tests
    {
        private readonly Material[] materials = {
            new Material { Name = "n0" },
            new Material { Name = "n1" },
            new Material { Name = "n2" },
            new Material { Name = "n3" },
            new Material { Name = "n4" },
            new Material { Name = "n5" },
            new Material { Name = "n6" }
        };

        [TestMethod]
        public void Test_first_page()
        {
            var model = new DisplayAllViewModel(3, 1, this.materials.Count(), this.materials.Take(3));

            Assert.AreEqual(3, model.Materials.Count(), "Wrong materials count.");
            Assert.AreEqual("n0", model.Materials.First().Name, "The first element is wrong.");
            Assert.AreEqual("n1", model.Materials.Skip(1).First().Name, "The second element is wrong.");
            Assert.AreEqual("n2", model.Materials.Skip(2).First().Name, "The third element is wrong.");
            Assert.AreEqual(1, model.CurrentPageNumber, "The number of current page is invalid.");
            Assert.AreEqual(3, model.TotalPagesCount, "The pages count is iinvalid.");
        }

        [TestMethod]
        public void Test_second_page()
        {
            var model = new DisplayAllViewModel(3, 2, this.materials.Count(), this.materials.Skip(3).Take(3));

            Assert.AreEqual(3, model.Materials.Count(), "Wrong materials count.");
            Assert.AreEqual("n3", model.Materials.First().Name, "The first element is wrong.");
            Assert.AreEqual("n4", model.Materials.Skip(1).First().Name, "The second element is wrong.");
            Assert.AreEqual("n5", model.Materials.Skip(2).First().Name, "The third element is wrong.");
            Assert.AreEqual(2, model.CurrentPageNumber, "The number of current page is invalid.");
            Assert.AreEqual(3, model.TotalPagesCount, "The pages count is iinvalid.");
        }

        [TestMethod]
        public void Test_second_not_full_page()
        {
            var model = new DisplayAllViewModel(3, 3, this.materials.Count(), this.materials.Skip(6).Take(3));

            Assert.AreEqual(1, model.Materials.Count(), "Wrong materials count.");
            Assert.AreEqual("n6", model.Materials.First().Name, "The first element is wrong.");
            Assert.AreEqual(3, model.CurrentPageNumber, "The number of current page is invalid.");
            Assert.AreEqual(3, model.TotalPagesCount, "The pages count is iinvalid.");
        }

        [TestMethod]
        public void Test_empty_page()
        {
            var model = new DisplayAllViewModel(3, 4, this.materials.Count(), this.materials.Skip(9).Take(3));

            Assert.AreEqual(0, model.Materials.Count(), "Wrong materials count.");
            Assert.AreEqual(4, model.CurrentPageNumber, "The number of current page is invalid.");
            Assert.AreEqual(3, model.TotalPagesCount, "The pages count is iinvalid.");
        }

        [TestMethod]
        public void Test_invalid_pageNumber()
        {
            var model = new DisplayAllViewModel(3, -1, this.materials.Count(), this.materials.Take(3));

            Assert.AreEqual(3, model.Materials.Count(), "Wrong materials count.");
            Assert.AreEqual("n0", model.Materials.First().Name, "The first element is wrong.");
            Assert.AreEqual("n1", model.Materials.Skip(1).First().Name, "The second element is wrong.");
            Assert.AreEqual("n2", model.Materials.Skip(2).First().Name, "The third element is wrong.");
            Assert.AreEqual(1, model.CurrentPageNumber, "The number of current page is invalid.");
            Assert.AreEqual(3, model.TotalPagesCount, "The pages count is iinvalid.");
        }
    }
}
