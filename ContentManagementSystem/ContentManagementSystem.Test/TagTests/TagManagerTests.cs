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

namespace ContentManagementSystem.Test.TagTests
{
    [TestFixture]
    internal class TagManagerTests
    {
        private readonly ITagManager _tagManager;
        private readonly Tag _testTag;

        public TagManagerTests()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _tagManager = kernel.Get<ITagManager>();
            _testTag = new Tag { Id = 1, Name = "Subaru" };
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanAddTag()
        {
            Response<int> actual = _tagManager.Add("Running");

            Assert.AreEqual(3, _tagManager.GetAll().Data.Count);
            Assert.AreEqual("Running", _tagManager.GetAll().Data[2].Name);
            Assert.AreEqual(3, actual.Data);
        }

        [Test]
        public void CanUpdateTag()
        {
            _tagManager.Update(_testTag);

            Response<List<Tag>> actual = _tagManager.GetAll();

            Assert.AreEqual("Subaru", actual.Data[0].Name);
        }

        [Test]
        public void CanRemoveTag()
        {
            _tagManager.Remove(1);

            Assert.IsFalse(_tagManager.GetAll().Data.Select(t => t.Id).Contains(1));
        }

        [Test]
        public void CanGetById()
        {
            Response<Tag> actual = _tagManager.GetById(2);

            Assert.AreEqual(2, actual.Data.Id);
            Assert.AreEqual("Cleveland", actual.Data.Name);
        }

        [Test]
        public void CanGetByName()
        {
            Response<Tag> actual = _tagManager.GetByName("cleveland");

            Assert.AreEqual("Cleveland", actual.Data.Name);
            Assert.AreEqual(2, actual.Data.Id);
        }

        [Test]
        public void CanGetAllTags()
        {
            Response<List<Tag>> actual = _tagManager.GetAll();

            Assert.AreEqual(2, actual.Data.Count);
            Assert.AreEqual("NASA", actual.Data[0].Name);
        }
    }
}
