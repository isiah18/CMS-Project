using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.NinjectBindings;
using ContentManagementSystem.Model;
using Ninject;

namespace ContentManagementSystem.UI.Controllers
{
    public class PostStatusController : ApiController
    {
        private readonly IPostStatusManager _statusManager;

        public PostStatusController()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _statusManager = kernel.Get<IPostStatusManager>();
        }

        // GET: api/Post
        public IEnumerable<PostStatus> Get()
        {
            var s = _statusManager.GetAll().Data;
            return s;
        }
    }
}
