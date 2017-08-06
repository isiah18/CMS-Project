using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.Managers;
using ContentManagementSystem.BLL.NinjectBindings;
using ContentManagementSystem.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;

namespace ContentManagementSystem.UI.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private readonly IStaticPageManager _staticPageManager;

        public RoleController()
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

        // GET: All Roles
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(IdentityRole Role)
        {
            var myRole = context.Roles.FirstOrDefault(m => m.Id == Role.Id);
            context.Roles.Remove(myRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AssignAccountRole()
        {
            var UsersAndRoles = new RolesAndUsersViewModel();
            if (User.IsInRole("Admin"))
            {
                UsersAndRoles.AllUsers = context.Users.ToList();
                UsersAndRoles.AllRoles = context.Roles.Where(r => r.Name != "Admin").ToList();
                foreach (var user in UsersAndRoles.AllUsers)
                {
                    if(user.Roles.Count == 0)
                     {
                        user.Roles.Add(new IdentityUserRole { RoleId = "0", UserId = user.Id });
                    }
                }
                context.SaveChanges();
                return View(UsersAndRoles);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveUserRole(string userId, string roleId)
        {
            var user = context.Users.Single(u => u.Id == userId);
            user.Roles.Clear();
            user.Roles.Add(new IdentityUserRole {RoleId = roleId, UserId = userId});

            context.SaveChanges();

            //SaveUserRoles(userName, roleId);
            return RedirectToAction("AssignAccountRole");
        }

        //private void SaveUserRoles(string userName, string roleId)
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

           
        //    try
        //    {
        //        var user = UserManager.FindByName(userName);
        //        var Roles = context.Roles.ToList();
        //        var RoleName = Roles.FirstOrDefault(m => m.Id == roleId).ToString();
        //        UserManager.AddToRole(user.Id, RoleName);
        //        context.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }

        //}

        [HttpPost]
        public ActionResult DeleteUser(string UserId)
        {
            RemoveUser(UserId);
            return RedirectToAction("AssignAccountRole");
        }

        private void RemoveUser(string userId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            try
            {
                var user = UserManager.FindById(userId);
                UserManager.Delete(user);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}