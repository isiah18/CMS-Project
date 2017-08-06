using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Data;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Data.NinjectBindings;
using ContentManagementSystem.Data.Repository;
using ContentManagementSystem.Model;
using Ninject;
using NUnit.Framework;

namespace ContentManagementSystem.Test
{
    [TestFixture]
    internal class InquiryRepositoryTest
    {
        private readonly IInquiryRepository _inquiryRepo;
        private readonly Inquiry _testInquiry;

        public InquiryRepositoryTest()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _inquiryRepo = kernel.Get<IInquiryRepository>();
            _testInquiry = new Inquiry();
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void GetAll()
        {
            var repo = new InquiryRepository();
            var inquiryList = repo.GetAll();

            Assert.AreEqual(1, inquiryList.Count);
        }

        [Test]
        public void GetSingleInquiry()
        {
            var actual = _inquiryRepo.Get(1);

            Assert.AreEqual(1, actual.Id);
        }


        [Test]
        public void AddInquiry()
        {
            Inquiry inquiry = new Inquiry()
            {

                InquiryStatusId = 1,
                Name = "Jake",
                Phone = "654-234-6453",
                Email = "Jake@Eamil.Com",
                Message = "Very Cool site"

            };

            _inquiryRepo.Add(inquiry);
            var actual = _inquiryRepo.GetAll();

            Assert.AreEqual(2, actual.Count);

        }

        [Test]
        public void DeleteInquiry()
        {
            _inquiryRepo.Delete(2);
            var actual = _inquiryRepo.GetAll().Count;

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void UpdateStatus()
        {
            Inquiry inquiry = new Inquiry();
            {
                inquiry.Id = 1;
                inquiry.InquiryStatusId = 2;
            }
            _inquiryRepo.UpdateStatus(inquiry);
            var actual = _inquiryRepo.Get(1);

            Assert.AreEqual(2, actual.InquiryStatusId);
        }

        [Test]
        public void MarkAsUnread()
        {
            Inquiry inquiry = new Inquiry();
            {
                inquiry.Id = 1;
                inquiry.InquiryStatusId = 1;
            }
            _inquiryRepo.MarkAsUnread(inquiry);
            var actual = _inquiryRepo.Get(1);

            Assert.AreEqual(1, actual.InquiryStatusId);
        }
    }
}

