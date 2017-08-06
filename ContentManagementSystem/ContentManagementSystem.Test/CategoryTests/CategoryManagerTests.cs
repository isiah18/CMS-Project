using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.NinjectBindings;
using ContentManagementSystem.Model;
using Ninject;
using NUnit.Framework;

namespace ContentManagementSystem.Test.CategoryTests
{
    [TestFixture]
    class CategoryManagerTests
    {
        private readonly ICategoryManager _categoryManager;
        private readonly Category _testCategory;

        public CategoryManagerTests()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _categoryManager = kernel.Get<ICategoryManager>();
            _testCategory = new Category { Id = 1, Name = "TestCatFromMgr" };
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanAddCategory()
        {
            Response<int> actual = _categoryManager.Add("TestCatFromMgr");

            Assert.AreEqual(4, _categoryManager.GetAll().Data.Count);
            Assert.AreEqual("TestCatFromMgr", _categoryManager.GetAll().Data[3].Name);
            Assert.AreEqual(4, actual.Data);
        }

        [Test]
        public void CanUpdateCategory()
        {
            _categoryManager.Update(_testCategory);

            Response<List<Category>> actual = _categoryManager.GetAll();

            Assert.AreEqual("TestCatFromMgr", actual.Data[0].Name);
        }

        [Test]
        public void CanRemoveCategory()
        {
            _categoryManager.Remove(1);

            Assert.IsFalse(_categoryManager.GetAll().Data.Select(t => t.Id).Contains(1));
        }

        [Test]
        public void CanGetById()
        {
            Response<Category> actual = _categoryManager.GetById(2);

            Assert.AreEqual(2, actual.Data.Id);
            Assert.AreEqual(".NET", actual.Data.Name);
        }

        [Test]
        public void CanGetByName()
        {
            Response<Category> actual = _categoryManager.GetByName("jquery");

            Assert.AreEqual("jQuery", actual.Data.Name);
            Assert.AreEqual(3, actual.Data.Id);
        }

        [Test]
        public void CanGetAllCategories()
        {
            Response<List<Category>> actual = _categoryManager.GetAll();

            Assert.AreEqual(3, actual.Data.Count);
            Assert.AreEqual("Turtles", actual.Data[0].Name);
        }
    }
}
