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

namespace ContentManagementSystem.Test.BlogTests
{
    [TestFixture]
    public class BlogManagerTests
    {
        private readonly IBlogManager _blogManager;
        private readonly Blog _testBlog;

        public BlogManagerTests()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _blogManager = kernel.Get<IBlogManager>();
            _testBlog = new Blog
            {
                Title = "BLAH",
                TagLine = "BLAHH",
                ContactDescription = "BLAHHH"
            };
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanGetBlogData()
        {
            var actual = _blogManager.Get();

            Assert.AreEqual("this is our blog", actual.Data.TagLine);
        }

        [Test]
        public void CanUpdateBlog()
        {
            _blogManager.Update(_testBlog);

            Assert.AreEqual("BLAHHH", _blogManager.Get().Data.ContactDescription);
        } 
    }
}
