using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ContentManagementSystem.BLL;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.Managers;
using ContentManagementSystem.BLL.NinjectBindings;
using ContentManagementSystem.Model;
using Ninject;


namespace ContentManagementSystem.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogManager _blogManager;
        private readonly IStaticPageManager _staticPageManager;
        private readonly IPostManager _postManager;
        private readonly IInquiryManager _inquiryManager;
        //private readonly ICategoryManager _categoryManager;
        //private readonly ITagManager _tagManager;


        public HomeController()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _blogManager = kernel.Get<IBlogManager>();
            _staticPageManager = kernel.Get<IStaticPageManager>();
            _postManager = kernel.Get<IPostManager>();
            _inquiryManager = kernel.Get<IInquiryManager>();
            //_categoryManager = kernel.Get<ICategoryManager>();
            //_tagManager = kernel.Get<ITagManager>();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var r = _staticPageManager.GetAll();
            var r2 = _blogManager.Get();

            if (!r.Success || !r2.Success)
            {
                SetErrorViewData(r.Message);
            }
                
            ViewData["StaticPages"] = r.Data.Where(sp => sp.IsLive).ToList();
            ViewData["ContactPageMsg"] = r2.Data.ContactDescription;
        }

        public ActionResult Index()
        {
            Response<Blog> r = _blogManager.GetForIndex();

            if (!r.Success)
            {
                SetErrorViewData(r.Message);
            }

            return View(r.Data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please Fill Out The Information Below";

            var i = new Inquiry();

            return View (i);
        }

        [HttpPost]
        public ActionResult Contact(Inquiry inquiry)
        {
            if (ModelState.IsValid)
            {
                inquiry.InquiryStatusId = 1;

                var r = _inquiryManager.Add(inquiry);

                //Response<Blog> r = _blogManager.Get();

                if (!r.Success)
                {
                    SetErrorViewData(r.Message);
                    return View(inquiry);
                }
                return View("ConformationPage");
            }
            return View(inquiry);
        }

        //[HttpPost]
        //public ActionResult Delete(int inquiryId)
        //{
        //    Response<Inquiry> r = _inquiryManager.Get(inquiryId);

        //    if (!r.Success)
        //    {
        //        SetErrorTempData(r.Message);
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        var manager = new InquiryManager();
        //        manager.Delete(inquiryId);
        //        //Return View Needs to be Changed
        //        return View("ManageInquiry","");
        //    }
        //    //Also Needs to be changed
        //    return View("ManageInquiry");
        //}

        public ActionResult BlogPost(int id)
        {
            Response<Post> r = _postManager.Get(id);

            if (!r.Success)
            {
                SetErrorViewData(r.Message);
            }

            return View(r.Data);
        }

        private void SetErrorViewData(string message)
        {
            ViewData["Alert"] = "alert-danger";
            ViewData["Message"] = message;
        }
    }
}