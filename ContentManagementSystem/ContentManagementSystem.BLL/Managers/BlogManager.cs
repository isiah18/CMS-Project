using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.Data;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Data.NinjectBindings;
using ContentManagementSystem.Model;
using Ninject;

namespace ContentManagementSystem.BLL.Managers
{
    public class BlogManager : IBlogManager
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ITagsOnPostsRepository _tagsOnPostsRepository;
        private readonly IExceptionsRepository _exceptionsRepository;
        private readonly ICategoriesOnPostsRepository _categoriesOnPostsRepository;
        private readonly IStaticPageRepository _staticPageRepository;

        public BlogManager()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _blogRepository = kernel.Get<IBlogRepository>();
            _postRepository = kernel.Get<IPostRepository>();
            _categoryRepository = kernel.Get<ICategoryRepository>();
            _categoriesOnPostsRepository = kernel.Get<ICategoriesOnPostsRepository>();
            _tagRepository = kernel.Get<ITagRepository>();
            _tagsOnPostsRepository = kernel.Get<ITagsOnPostsRepository>();
            _exceptionsRepository = kernel.Get<IExceptionsRepository>();
            _staticPageRepository = kernel.Get<IStaticPageRepository>();
        }

        public Response<Blog> Update(Blog b)
        {
            var r = new Response<Blog>();

            try
            {
                _blogRepository.Update(b);
                r.Success = true;
                r.Message = "Updated blog data";
                r.Data = new Blog();
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to update blog data.";
                r.Data = new Blog();
            }
            return r;
        }

        public Response<Blog> Get()
        {
            var r = new Response<Blog>();

            try
            {
                r.Success = true;
                r.Message = "Loaded blog data.";
                r.Data = _blogRepository.Get();
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load blog data.";
                r.Data = new Blog();
            }
            return r;
        }

        public Response<Blog> GetForIndex()
        {
            var r = new Response<Blog>();
            var cloud = new Dictionary<Tag, int>();

            try
            {
                foreach (Tag t in _tagRepository.GetAll())
                {
                    cloud.Add(t, _tagsOnPostsRepository.GetTotalCountById(t.Id));
                }
                r.Success = true;
                r.Message = "Loaded blog data.";
                r.Data = _blogRepository.Get();
                r.Data.TotalNumberOfPosts = _postRepository.GetTotalPostCountByStatus("Approved");
                r.Data.StaticPages = _staticPageRepository.GetAll().Where(sp => sp.IsLive == true).ToList();
                r.Data.InitialFivePost = _postRepository.GetPostsPaginationByStatus(0, 5, "Approved");

                foreach (Post p in r.Data.InitialFivePost)
                {
                    p.CategoriesOnPost = _categoriesOnPostsRepository.GetAllCategoriesForPost(p.Id);
                    p.TagsOnPost = _tagsOnPostsRepository.GetAllTagsForPost(p.Id);
                }
                r.Data.Categories = _categoryRepository.GetAll();
                r.Data.TagCloud = cloud;
                r.Data.StaticPages = new List<StaticPage>();
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load blog data.";
                r.Data = new Blog();
            }
            return r;
        }

    }
}
