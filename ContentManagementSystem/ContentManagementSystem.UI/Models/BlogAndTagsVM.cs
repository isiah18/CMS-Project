using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.UI.Models
{
    public class BlogAndTagsVM
    {
        public Blog CurrentBlog { get; set; }
        public List<Tag> AllTags { get; set; } 
    }
}