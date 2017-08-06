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

namespace ContentManagementSystem.Test.TagTests
{
    [TestFixture]
    public class TagRepositoryTests
    {
        private readonly ITagRepository _tagRepository;
        private readonly Tag _testTag;

        public TagRepositoryTests()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _tagRepository = kernel.Get<ITagRepository>();
            _testTag = new Tag {Id=1, Name = "Subaru"};
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanAddTag()
        {
            int actual = _tagRepository.Add("Running");

            Assert.AreEqual(3, _tagRepository.GetAll().Count);
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void CanUpdateTag()
        {
            _tagRepository.Update(_testTag);

            var actual = _tagRepository.GetAll();

            Assert.AreEqual("Subaru", actual[0].Name);
        }

        [Test]
        public void CanRemoveTag()
        {
            _tagRepository.Remove(1);

            Assert.IsFalse(_tagRepository.GetAll().Select(t => t.Id).Contains(1));
        }

        [Test]
        public void CanGetById()
        {
            var actual = _tagRepository.GetById(2);

            Assert.AreEqual(2, actual.Id);
            Assert.AreEqual("Cleveland", actual.Name);
        }

        [Test]
        public void CanGetByName()
        {
            var actual = _tagRepository.GetByName("cleveland");

            Assert.AreEqual("Cleveland", actual.Name);
            Assert.AreEqual(2, actual.Id);
        }

        [Test]
        public void CanGetAllTags()
        {
            List<Tag> actual = _tagRepository.GetAll();

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("NASA", actual[0].Name);
        }
    }
}
