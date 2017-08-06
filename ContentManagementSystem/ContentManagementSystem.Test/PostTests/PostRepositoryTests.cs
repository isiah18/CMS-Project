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

namespace ContentManagementSystem.Test.PostTests
{
    [TestFixture]
    internal class PostRepositoryTests
    {
        private readonly IPostRepository _postRepo;
        private readonly Post _testPost;

        public PostRepositoryTests()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _postRepo = kernel.Get<IPostRepository>();
            _testPost = new Post
            {
                Title = "Test Post",
                Content = "Test!",
                CreatedDate = DateTime.Now,
                PublishDate = DateTime.Now,
                Status = new PostStatus(),
            };
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanGetPostsPaginationByStatusAndTag()
        {
            var actual = _postRepo.GetPostsPaginationByStatusAndTag(0, 5, "Approved", 2);

            Assert.AreEqual(3, actual.Count);
        }

        [Test]
        public void CanGetPostsPaginationByStatusAndCategory()
        {
            var actual = _postRepo.GetPostsPaginationByStatusAndCategory(0, 5, "Approved", 3);

            Assert.AreEqual(4, actual.Count);
        }

        [Test]
        public void CanGetTotalPostCountByStatus()
        {
            int actual = _postRepo.GetTotalPostCountByStatus("Approved");

            Assert.AreEqual(6, actual);
        }

        [Test]
        public void CanGetAllPostFromDb()
        {
            List<Post> actual = _postRepo.GetAll();

            Assert.AreEqual(6, actual.Count);
        }

        [Test]
        public void CanGetPage()
        {
            List<Post> actual = _postRepo.GetPostsPaginationByStatus(3, 5, "Approved");

            Assert.AreEqual(3, actual.Count);
        }

        [Test]
        public void CanGetSinglePostFromDb()
        {
            Post actual = _postRepo.Get(1);

            Assert.AreEqual(1, actual.Id);
        }
    }
}
