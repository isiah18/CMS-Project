using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.NinjectBindings;
using Ninject;

namespace ContentManagementSystem.UI.Controllers
{
    public class PageController : Controller
    {
        private readonly IStaticPageManager _staticPageManager;

        public PageController()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _staticPageManager = kernel.Get<IStaticPageManager>();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var r = _staticPageManager.GetAll();

            if (!r.Success)
            {
                SetErrorViewData(r.Message);
            }

            ViewData["StaticPages"] = r.Data.Where(sp => sp.IsLive).ToList();
        }

        private void SetErrorViewData(string message)
        {
            ViewData["Alert"] = "alert-danger";
            ViewData["Message"] = message;
        }
        // GET: StaticPage
        public ActionResult Index(int id)
        {
            var r = _staticPageManager.GetById(id);

            if(!r.Success)
                SetErrorViewData(r.Message);
            return View(r.Data);
        }
    }
}