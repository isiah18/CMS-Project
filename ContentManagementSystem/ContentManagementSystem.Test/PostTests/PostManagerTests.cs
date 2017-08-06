using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.NinjectBindings;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Data.NinjectBindings;
using ContentManagementSystem.Model;
using Ninject;
using NUnit.Framework;

namespace ContentManagementSystem.Test.PostTests
{
    [TestFixture]
    public class PostManagerTests
    {
        private readonly IPostManager _postManager;
        private readonly Post _testPost;

        public PostManagerTests()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _postManager = kernel.Get<IPostManager>();
            _testPost = new Post();
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void ManagerCanGetPostById()
        {
            var actual = _postManager.Get(1);

            Assert.AreEqual(1, actual.Data.Id);
        }

        [Test]
        public void CanGetPostsPaginationByStatusAndTag()
        {
            var actual = _postManager.GetPostsPaginationByStatusAndTag(0, 5, "Approved", 2);

            Assert.AreEqual(3, actual.Data.Count);
        }

        [Test]
        public void CanGetPostsPaginationByStatusAndCategory()
        {
            var actual = _postManager.GetPostsPaginationByStatusAndCategory(0, 5, "Approved", 3);

            Assert.AreEqual(4, actual.Data.Count);
        }

        [Test]
        public void CanGetPostsPaginationByStatus()
        {
            var actual = _postManager.GetPostsPaginationByStatus(0, 5, "Approved");

            Assert.AreEqual(5, actual.Data.Count);
        }

        [Test]
        public void CanGetTotalPostCountByStatus()
        {
            var actual = _postManager.GetTotalPostCountByStatus("Approved");

            Assert.AreEqual(6, actual.Data);
        }

        [Test]
        public void ManagerCanGetByStatus()
        {
            var actual = _postManager.GetAllByStatus("Approved");

            Assert.AreEqual(6, actual.Data.Count);
            Assert.AreEqual(3, actual.Data[0].CategoriesOnPost.Count);
        }

        [Test]
        public void CanAddPost()
        {
            _testPost.Content = "Blah.";
            _testPost.CreatedDate = DateTime.Now;
            _testPost.PublishDate = DateTime.Now;
            _testPost.Title = "Test post";
            _testPost.Author = new Author {Id = "8b1603b6-0d7a-46d8-a2eb-df064216a29a" };

            _postManager.AddPost(_testPost, new[] {"#neato"}, new[] {".NET"}, "Approved");

            Assert.AreEqual(7, _postManager.GetAllByStatus("Approved").Data.Count);
        }
    }
}
