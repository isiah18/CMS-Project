using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Data.NinjectBindings;
using Ninject;
using NUnit.Framework;

namespace ContentManagementSystem.Test.TagsOnPostTests
{
    [TestFixture]
    internal class TagsOnPostsRepositoryTests
    {
        private readonly ITagsOnPostsRepository _tagsOnPostsRepository;

        public TagsOnPostsRepositoryTests()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _tagsOnPostsRepository = kernel.Get<ITagsOnPostsRepository>();
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanGetPostCountForTag()
        {
            int actual = _tagsOnPostsRepository.GetTotalCountById(2);

            Assert.AreEqual(3, actual);
        }
    }
}
