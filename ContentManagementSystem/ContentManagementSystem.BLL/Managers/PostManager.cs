using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Data.NinjectBindings;
using ContentManagementSystem.Model;
using Ninject;

namespace ContentManagementSystem.BLL.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepository _postRepo;
        private readonly IPostStatusRepository _postStatusRepo;
        private readonly ITagRepository _tagRepo;
        private readonly ITagsOnPostsRepository _tagOnPostRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICategoriesOnPostsRepository _categoryOnPostRepo;
        private readonly IExceptionsRepository _exceptionsRepository;

        public PostManager()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _postRepo = kernel.Get<IPostRepository>();
            _postStatusRepo = kernel.Get<IPostStatusRepository>();
            _tagRepo = kernel.Get<ITagRepository>();
            _tagOnPostRepo = kernel.Get<ITagsOnPostsRepository>();
            _categoryRepo = kernel.Get<ICategoryRepository>();
            _categoryOnPostRepo = kernel.Get<ICategoriesOnPostsRepository>();
            _exceptionsRepository = kernel.Get<IExceptionsRepository>();
        }

        public Response<List<Post>> GetPostsPaginationByStatusAndCategory(int offset, int maxAmount, string status, int categoryId)
        {
            var r = new Response<List<Post>>();
            try
            {
                r.Success = true;
                r.Message = "Loaded posts.";
                r.Data = _postRepo.GetPostsPaginationByStatusAndCategory(offset, maxAmount, status, categoryId);

                foreach (Post p in r.Data)
                {
                    p.CategoriesOnPost = _categoryOnPostRepo.GetAllCategoriesForPost(p.Id);
                    p.TagsOnPost = _tagOnPostRepo.GetAllTagsForPost(p.Id);
                }
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load posts.";
                r.Data = new List<Post>();
            }
            return r;
        }

        public Response<List<Post>> GetPostsPaginationByStatusAndTag(int offset, int maxAmount, string status, int tagId)
        {
            var r = new Response<List<Post>>();
            try
            {
                r.Success = true;
                r.Message = "Loaded posts.";
                r.Data = _postRepo.GetPostsPaginationByStatusAndTag(offset, maxAmount, status, tagId);

                foreach (Post p in r.Data)
                {
                    p.CategoriesOnPost = _categoryOnPostRepo.GetAllCategoriesForPost(p.Id);
                    p.TagsOnPost = _tagOnPostRepo.GetAllTagsForPost(p.Id);
                }
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load posts.";
                r.Data = new List<Post>();
            }
            return r;
        }

        public Response<List<Post>> GetPostsPaginationByStatus(int offset, int maxAmount, string status)
        {
            var r = new Response<List<Post>>();
            try
            {
                r.Success = true;
                r.Message = "Loaded posts.";
                r.Data = _postRepo.GetPostsPaginationByStatus(offset, maxAmount, status);

                foreach (Post p in r.Data)
                {
                    p.CategoriesOnPost = _categoryOnPostRepo.GetAllCategoriesForPost(p.Id);
                    p.TagsOnPost = _tagOnPostRepo.GetAllTagsForPost(p.Id);
                }
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load posts.";
                r.Data = new List<Post>();
            }
            return r;
        }

        public Response<Post> Get(int id)
        {
            var r = new Response<Post>();
            var post = new Post();
            var tagsPost = new List<Tag>();
            var categoryPost = new List<Category>();
            try
            {
                post = _postRepo.Get(id);
                List<int> tagIdList = _tagOnPostRepo.GetAllTagIdsForPost(id);
                List<int> catIdList = _categoryOnPostRepo.GetAllCategoryIdsForPost(id);
                foreach (var tag in tagIdList)
                {
                    tagsPost.Add(_tagRepo.GetById(tag));
                }
                foreach (var cat in catIdList)
                {
                    categoryPost.Add(_categoryRepo.GetById(cat));
                }
                post.TagsOnPost = tagsPost;
                post.CategoriesOnPost = categoryPost;
                r.Success = true;
                r.Message = "Loaded post.";
                r.Data = post;
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load post.";
                r.Data = new Post();
            }
            return r;
        }

        public Response<int> GetTotalPostCountByStatus(string status)
        {
            var r = new Response<int>();
            try
            {
                r.Success = true;
                r.Message = "Loaded total post count.";
                r.Data = _postRepo.GetTotalPostCountByStatus(status);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load total post count.";
                r.Data = 0;
            }
            return r;
        }

        public Response<int> GetTotalPostCountByStatusAndTag(string status, int tagId)
        {
            var r = new Response<int>();
            try
            {
                r.Success = true;
                r.Message = "Loaded total post count.";
                r.Data = _postRepo.GetTotalPostCountByStatusAndTag(status, tagId);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load total post count.";
                r.Data = 0;
            }
            return r;
        }

        public Response<int> GetTotalPostCountByStatusAndCategory(string status, int catId)
        {
            var r = new Response<int>();
            try
            {
                r.Success = true;
                r.Message = "Loaded total post count.";
                r.Data = _postRepo.GetTotalPostCountByStatusAndCategory(status, catId);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load total post count.";
                r.Data = 0;
            }
            return r;
        }

        public Response<List<Post>> GetAllByStatus(string status, string userId = null)
        {
            var response = new Response<List<Post>>();
            try
            {
                List<Post> results = _postRepo.GetAll();
                if (userId == null)
                {
                    response.Data = results.Where(p => p.Status.Status == status).ToList();
                }
                else
                {
                    response.Data = results.Where(p => p.Status.Status == status && p.Author.Id == userId).ToList();
                }
                foreach (var post in response.Data)
                {
                    post.CategoriesOnPost = _categoryOnPostRepo.GetAllCategoriesForPost(post.Id);
                    post.TagsOnPost = _tagOnPostRepo.GetAllTagsForPost(post.Id);
                }
                response.Success = true;
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
            }
            return response;
        }

        public Response<Post> DeletePost(int id)
        {
            var response = new Response<Post>();
            try
            {
                _postRepo.Remove(id);
                response.Success = !_postRepo.GetAll().Exists(p => p.Id == id);
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }

        public Response<Post> UpdatePost(Post post, string[] tagArr, string[] catArr, string status)
        {
            var response = new Response<Post>();
            try
            {
                Post oldPost = new Post();
                oldPost = _postRepo.Get(post.Id);
                post.CreatedDate = oldPost.CreatedDate;
                post.Author = _postRepo.GetAuthorByPost(post.Id);
                _postRepo.Remove(post.Id);
                _categoryOnPostRepo.Remove(post.Id);
                _tagOnPostRepo.Remove(post.Id);
                response = AddPost(post, tagArr, catArr, status);
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }

        //todo Should I change this to query the GetAll once and use linq to see if tag is already in database?
        public Response<Post> AddPost(Post post, string[] tagArr, string[] catArr, string status)
        {
            var response = new Response<Post>();
            try
            {
                post.Status = _postStatusRepo.GetById(status);
                int postId = _postRepo.Add(post);
                if (tagArr != null)
                {
                    foreach (var tag in tagArr)
                    {
                        if (tag == null)
                        {
                            continue;
                        }
                        Tag getTag = _tagRepo.GetByName(tag);
                        if (getTag != null)
                        {
                            _tagOnPostRepo.Add(getTag.Id, postId);
                        }
                        else
                        {
                            int tagId = _tagRepo.Add(tag);
                            _tagOnPostRepo.Add(tagId, postId);
                        }
                    }
                }
                if (catArr != null)
                {
                    foreach (var category in catArr)
                    {
                        if (category == null)
                        {
                            continue;
                        }
                        Category getCategory = _categoryRepo.GetByName(category);
                        _categoryOnPostRepo.Add(getCategory.Id, postId);
                    }
                }

                response.Data = post;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
