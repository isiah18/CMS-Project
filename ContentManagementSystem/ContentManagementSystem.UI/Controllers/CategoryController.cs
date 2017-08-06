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
    public class CategoryController : ApiController
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _categoryManager = kernel.Get<ICategoryManager>();
        }

        // GET: api/Category
        public IEnumerable<Category> Get()
        {
            var r = _categoryManager.GetAll();
            //if (!r.Success)
            //{
            //    SetErrorTempData(r.Message);
            //}
            return r.Data;
        }

        public HttpResponseMessage Post(Category category)
        {
            var r = _categoryManager.Add(category.Name);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
