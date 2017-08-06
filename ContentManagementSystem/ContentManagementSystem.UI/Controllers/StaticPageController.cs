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
    public class StaticPageController : ApiController
    {
        private readonly IStaticPageManager _staticPageManager;

        public StaticPageController()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _staticPageManager = kernel.Get<IStaticPageManager>();
        }

        public List<StaticPage> Get()
        {
            return _staticPageManager.GetAll().Data;
        }

        public StaticPage Get(int id)
        {
            return _staticPageManager.GetById(id).Data;
        }

        public HttpResponseMessage Post(StaticPage sp)
        {
            var r = _staticPageManager.Add(sp);

            if (r.Success)
                return Request.CreateResponse(HttpStatusCode.Created, r.Data);
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        public HttpResponseMessage Put(StaticPage sp)
        {
            //todo check if sp exists
            var r = _staticPageManager.Update(sp);

            if (r.Success)
                return Request.CreateResponse(HttpStatusCode.OK, r.Data);
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        public HttpResponseMessage Delete(int id)
        {
            var r = _staticPageManager.Remove(id);

            if (r.Success)
                return Request.CreateResponse(HttpStatusCode.OK, r.Data);
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}
