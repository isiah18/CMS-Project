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

namespace ContentManagementSystem.Test.StaticPageTests
{
    [TestFixture]
    public class StaticPageRepositoryTests
    {
        private IStaticPageRepository _staticPageRepository;
        private readonly StaticPage _testStaticPage;

        public StaticPageRepositoryTests()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _staticPageRepository = kernel.Get<IStaticPageRepository>();
            _testStaticPage = new StaticPage { IsLive = true, Title = "TESTPAGE22", Content = "THIS IS A TEST PAGE", ImagePath = "PATH"};
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanGetAll()
        {
            List<StaticPage> actual = _staticPageRepository.GetAll();

            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public void CanGetByTitle()
        {
            var actual = _staticPageRepository.GetByTitle("Test Static Page");

            Assert.AreEqual(1, actual.Id);
        }

        [Test]
        public void CanGetById()
        {
            var actual = _staticPageRepository.GetById(1);

            Assert.AreEqual("Test Static Page", actual.Title);
        }

        [Test]
        public void CanRemove()
        {
            _staticPageRepository.Remove(1);

            Assert.IsEmpty(_staticPageRepository.GetAll());
        }

        [Test]
        public void CanUpdate()
        {
            StaticPage sp = _staticPageRepository.GetById(1);
            sp.Title = "UPDATED TITLE";
            sp.ImagePath = "BLAH/BLAH/blah.jpg";
            _staticPageRepository.Update(sp);

            StaticPage actual = _staticPageRepository.GetById(1);

            Assert.AreEqual("UPDATED TITLE", actual.Title);
            Assert.AreEqual("BLAH/BLAH/blah.jpg", actual.ImagePath);
        }

        [Test]
        public void CanAdd()
        {
            int id = _staticPageRepository.Add(_testStaticPage);

            StaticPage actual = _staticPageRepository.GetById(id);

            Assert.AreEqual(_testStaticPage.Title, actual.Title);
            Assert.AreEqual("PATH", actual.ImagePath);
        }
    }
}
