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

namespace ContentManagementSystem.Test.StaticPageTests
{
    [TestFixture]
    public class StaticPageManagerTests
    {
        private readonly IStaticPageManager _staticPageManager;
        private readonly StaticPage _testStaticPage;

        public StaticPageManagerTests()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _staticPageManager = kernel.Get<IStaticPageManager>();
            _testStaticPage = new StaticPage {Title = "TESTstaticPAGEfromMgr", Content = "MgrTest", IsLive = true, ImagePath = "Blah/"};
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanGetAll()
        {
            Response<List<StaticPage>> actual = _staticPageManager.GetAll();

            Assert.AreEqual(1, actual.Data.Count);
        }

        [Test]
        public void CanGetByTitle()
        {
            Response<StaticPage> actual = _staticPageManager.GetByTitle("Test Static Page");

            Assert.AreEqual(1, actual.Data.Id);
        }

        [Test]
        public void CanGetById()
        {
            Response<StaticPage> actual = _staticPageManager.GetById(1);

            Assert.AreEqual("Test Static Page", actual.Data.Title);
        }

        [Test]
        public void CanRemove()
        {
            _staticPageManager.Remove(1);

            Assert.IsEmpty(_staticPageManager.GetAll().Data);
        }

        [Test]
        public void CanUpdate()
        {
            StaticPage sp = _staticPageManager.GetById(1).Data;
            sp.Title = "UPDATED TITLE";
            sp.ImagePath = "BLAH/BLAH/blah.jpg";
            _staticPageManager.Update(sp);

            StaticPage actual = _staticPageManager.GetById(1).Data;

            Assert.AreEqual("UPDATED TITLE", actual.Title);
            Assert.AreEqual("BLAH/BLAH/blah.jpg", actual.ImagePath);
        }

        [Test]
        public void CanAdd()
        {
            int id = _staticPageManager.Add(_testStaticPage).Data;

            StaticPage actual = _staticPageManager.GetById(id).Data;

            Assert.AreEqual(_testStaticPage.Title, actual.Title);
            Assert.AreEqual("Blah/", actual.ImagePath);
        }
    }
}
