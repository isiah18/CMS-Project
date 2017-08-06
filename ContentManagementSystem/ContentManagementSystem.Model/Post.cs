using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ContentManagementSystem.Model
{
    public class Post
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        public PostStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ExpireDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Category> CategoriesOnPost { get; set; }
        public List<Tag> TagsOnPost { get; set; }
    }
}