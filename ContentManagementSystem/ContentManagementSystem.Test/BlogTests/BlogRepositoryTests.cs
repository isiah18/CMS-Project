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

namespace ContentManagementSystem.Test.BlogTests
{
    [TestFixture]
    internal class BlogRepositoryTests
    {
        private readonly IBlogRepository _blogRepo;
        private readonly Blog _testBlog;

        public BlogRepositoryTests()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _blogRepo = kernel.Get<IBlogRepository>();
            _testBlog = new Blog();
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanGetBlog()
        {
            Blog actual = _blogRepo.Get();

            Assert.AreEqual("JIST blog", actual.Title);
        }

        [Test]
        public void CanUpdateBlog()
        {
            _testBlog.TagLine = "TEST TAG LINE";
            _testBlog.Title = "NEW TEST TITLE";
            _testBlog.ContactDescription = "TEST CONTACT DESCRIP";

            _blogRepo.Update(_testBlog);

            Assert.AreEqual("TEST TAG LINE", _blogRepo.Get().TagLine);
        }
    }
}
