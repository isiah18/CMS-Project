using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.Managers;
using Ninject.Modules;

namespace ContentManagementSystem.BLL.NinjectBindings
{
    public class BllBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IPostManager>().To<PostManager>();
            Bind<IBlogManager>().To<BlogManager>();
            Bind<ITagManager>().To<TagManager>();
            Bind<IPostStatusManager>().To<PostStatusManager>();
            Bind<ICategoryManager>().To<CategoryManager>();
            Bind<IInquiryManager>().To<InquiryManager>();
            Bind<IStaticPageManager>().To<StaticPageManager>();
        }
    }
}
