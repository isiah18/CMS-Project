using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Data.NinjectBindings;
using ContentManagementSystem.Model;
using Ninject;
using NUnit.Framework;

namespace ContentManagementSystem.Test.CategoryTests
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly Category _testCategory;

        public CategoryRepositoryTests()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _categoryRepository = kernel.Get<ICategoryRepository>();
            _testCategory = new Category { Id = 1, Name = "TestCatFromRepo" };
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanAddCategory()
        {
            int actual = _categoryRepository.Add("TestCatFromRepo");

            Assert.AreEqual(4, _categoryRepository.GetAll().Count);
            Assert.AreEqual("TestCatFromRepo", _categoryRepository.GetAll()[3].Name);
            Assert.AreEqual(4, actual);
        }

        [Test]
        public void CanUpdateCategory()
        {
            _categoryRepository.Update(_testCategory);

            var actual = _categoryRepository.GetAll();

            Assert.AreEqual("TestCatFromRepo", actual[0].Name);
        }

        [Test]
        public void CanRemoveCategory()
        {
            _categoryRepository.Remove(1);

            Assert.IsFalse(_categoryRepository.GetAll().Select(t => t.Id).Contains(1));
        }

        [Test]
        public void CanGetById()
        {
            var actual = _categoryRepository.GetById(2);

            Assert.AreEqual(2, actual.Id);
            Assert.AreEqual(".NET", actual.Name);
        }

        [Test]
        public void CanGetByName()
        {
            var actual = _categoryRepository.GetByName("jquery");

            Assert.AreEqual("jQuery", actual.Name);
            Assert.AreEqual(3, actual.Id);
        }

        [Test]
        public void CanGetAllCategories()
        {
            List<Category> actual = _categoryRepository.GetAll();

            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual("Turtles", actual[0].Name);
        }
    }
}
