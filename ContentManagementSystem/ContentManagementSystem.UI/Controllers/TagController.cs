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
    public class TagController : ApiController
    {
        private readonly ITagManager _tagManager;

        public TagController()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _tagManager = kernel.Get<ITagManager>();
        }

        // GET: api/Tag
        public IEnumerable<Tag> Get()
        {
            var r = _tagManager.GetAll();
            if (!r.Success)
            {
                
            }

            return r.Data;
        }

        //// GET: api/Tag/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Tag
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Tag/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Tag/5
        //public void Delete(int id)
        //{
        //}
    }
}
