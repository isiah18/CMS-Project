using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ContentManagementSystem.UI.Models
{
    public class RolesAndUsersViewModel
    {
        //ApplicationDbContext context = new ApplicationDbContext();

        public List<ApplicationUser> AllUsers { get; set; }
        public List<IdentityRole> AllRoles { get; set; }

        public IEnumerable<SelectListItem> RolesDropDown => new SelectList(AllRoles, "Id", "Name");

        //public IEnumerable<SelectListItem> AllRoles()
        //{
        //    var allRoles = context.Roles.ToList();
        //    var selectRoles = new List<SelectListItem>();
        //    foreach (var role in allRoles)
        //    {
        //        selectRoles.Add(new SelectListItem {Text = role.Name, Value = role.Id});
        //    }
        //    return selectRoles;
        //}

        public int RoleId { get; set; }


    }
}