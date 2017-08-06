using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.UI.Models
{
    public class PostCategoryTagVM
    {
        public string Status { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] TagList { get; set; }
        public string[] CategoryList { get; set; }
        public int Id { get; set; }
    }
}