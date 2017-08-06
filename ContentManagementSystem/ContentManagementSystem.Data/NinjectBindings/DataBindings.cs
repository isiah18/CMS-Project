using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Data.Repository;
using Ninject.Modules;

namespace ContentManagementSystem.Data.NinjectBindings
{
    public class DataBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IPostRepository>().To<PostRepository>();
            Bind<IInquiryRepository>().To<InquiryRepository>();
            Bind<IPostStatusRepository>().To<PostStatusRepository>();
            Bind<IBlogRepository>().To<BlogRepository>();
            Bind<ICategoryRepository>().To<CategoryRepository>();
            Bind<ITagsOnPostsRepository>().To<TagOnPostRepository>();
            Bind<ICategoriesOnPostsRepository>().To<CategoryOnPostRepository>();
            Bind<ITagRepository>().To<TagRepository>();
            Bind<IExceptionsRepository>().To<ExceptionsRepository>();
            Bind<IStaticPageRepository>().To<StaticPageRepository>();
        }
    }
}
