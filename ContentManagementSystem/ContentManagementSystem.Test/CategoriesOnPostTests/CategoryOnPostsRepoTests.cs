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

namespace ContentManagementSystem.Test.CategoriesOnPostTests
{
    [TestFixture]
    public class CategoryOnPostsRepoTests
    {
        private readonly ICategoriesOnPostsRepository _categoryOnPostsRepository;
        private readonly Category _testCategory;

        public CategoryOnPostsRepoTests()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _categoryOnPostsRepository = kernel.Get<ICategoriesOnPostsRepository>();
            _testCategory = new Category { Id = 1, Name = "TestCatFromRepo" };
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanLoadCategoriesForPost()
        {
            List<Category> actual = _categoryOnPostsRepository.GetAllCategoriesForPost(1);

            Assert.AreEqual(3, actual.Count);
        }
    }
}
