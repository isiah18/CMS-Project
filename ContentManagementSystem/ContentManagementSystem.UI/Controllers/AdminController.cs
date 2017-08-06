using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.Managers;
using ContentManagementSystem.BLL.NinjectBindings;
using ContentManagementSystem.Model;
using ContentManagementSystem.UI.Models;
using Microsoft.AspNet.Identity;
using Ninject;

namespace ContentManagementSystem.UI.Controllers
{
    [Authorize(Roles = "Trainee, Staff, Admin")]
    public class AdminController : Controller
    {
        //private readonly IPostManager _postManager;
        private readonly IInquiryManager _inquiryManager;
        private readonly IStaticPageManager _staticPageManager;

        public AdminController()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            //_postManager = kernel.Get<IPostManager>();
            _inquiryManager = kernel.Get<IInquiryManager>();
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

        public ActionResult ManageInquiry()
        {
            if (User.IsInRole("Admin"))
            {
                List<Inquiry> allInquiries = _inquiryManager.Getall().Data;
                return View(allInquiries);
            }
            else
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult SingleInquiry(int id)
        {
            if (User.IsInRole("Admin"))
            {
                Inquiry singleInquiry = _inquiryManager.Get(id).Data;
                singleInquiry.InquiryStatusId = 2;
                _inquiryManager.Update(singleInquiry);
                
                return View(singleInquiry);
            }
            else
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Response<Inquiry> r = _inquiryManager.Get(Id);

            if (!r.Success)
            {
                SetErrorTempData(r.Message);
            }
            if (ModelState.IsValid)
            {
                var manager = new InquiryManager();
                manager.Delete(Id);
                return RedirectToAction("ManageInquiry", "Admin");
            }
            return RedirectToAction("ManageInquiry","Admin");
           }

        [HttpPost]
        public ActionResult MarkUnread(int id)
        {
            Response<Inquiry> m = _inquiryManager.Get(id);
            if (User.IsInRole("Admin"))
            {
                m.Data.InquiryStatusId = 1;
                _inquiryManager.Update(m.Data);

                return RedirectToAction("ManageInquiry");
            }
            else
            {
                return RedirectToAction("ManageInquiry");
            }
        }
  
        private void SetErrorTempData(string message)
        {
            TempData["Alert"] = "alert-danger";
            TempData["Message"] = message;
        }


        public ActionResult Index()
        {
            //if (User.IsInRole("Admin"))
            //{
            //List<Post> pendingPosts = _postManager.GetAllByStatus("PendingReview").Data;
            //return View(pendingPosts);
            //}
            //return View(_postManager.GetAllByStatus("Draft").Data.Where(p => p.Author.Id == User.Identity.GetUserId()).ToList());
            return View();
        }


        public ActionResult EditBlog(BlogAndTagsVM b)
        {

            var blogManager = new BlogManager();
            var tagManager = new TagManager();
            b.CurrentBlog = blogManager.Get().Data;

            return View(b);

        }

        [HttpPost]
        public ActionResult SaveBlogTitle(string title)
        {
            var blogManager = new BlogManager();
            var blogtoUpdate = blogManager.Get().Data;
            blogtoUpdate.Title = title;
            
            blogManager.Update(blogtoUpdate);
            return RedirectToAction("EditBlog");
        }

        [HttpPost]
        public ActionResult SaveTagLine(string tagline)
        {
            var blogManager = new BlogManager();
            var blogtoUpdate = blogManager.Get().Data;
            blogtoUpdate.TagLine = tagline;
            blogManager.Update(blogtoUpdate);
            return RedirectToAction("EditBlog");
        }

        [HttpPost]
        public ActionResult SaveContactDescription(string condesc)
        {
            var blogManager = new BlogManager();
            var blogtoUpdate = blogManager.Get().Data;
            blogtoUpdate.ContactDescription = condesc;
            blogManager.Update(blogtoUpdate);
            return RedirectToAction("EditBlog");
        }
    }
}