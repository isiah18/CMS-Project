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

namespace ContentManagementSystem.Test.ExceptionTests
{
    [TestFixture]
    public class ExceptionsRepositoryTests
    {
        private readonly IExceptionsRepository _exceptionsRepository;

        public ExceptionsRepositoryTests()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _exceptionsRepository = kernel.Get<IExceptionsRepository>();
        }

        [SetUp]
        public void TestSetUp()
        {
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanWriteException()
        {
            _exceptionsRepository.Add(new Exception("This is an error message."));

            Assert.AreEqual(1, _exceptionsRepository.GetAll().Count);
        }

        [Test]
        public void CanRemoveAllExceptions()
        {
            _exceptionsRepository.Add(new Exception("This is an error message."));
            _exceptionsRepository.RemoveAll();

            Assert.IsEmpty(_exceptionsRepository.GetAll());
        }
    }
}
