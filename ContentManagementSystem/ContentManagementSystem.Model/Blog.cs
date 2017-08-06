using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Model
{
    public class Blog
    {
        public string Title { get; set; }
        public string TagLine { get; set; }
        public string ContactDescription { get; set; }
        public int TotalNumberOfPosts { get; set; }
        public List<Post> InitialFivePost { get; set; }
        public List<Category> Categories { get; set; }
        public List<StaticPage> StaticPages { get; set; }
        public Dictionary<Tag, int> TagCloud { get; set; }

        public Blog()
        {
            InitialFivePost = new List<Post>();
            Categories = new List<Category>();
            StaticPages = new List<StaticPage>();
            TagCloud = new Dictionary<Tag, int>();
        }
    }

    
}
