using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.BLL.NinjectBindings;
using ContentManagementSystem.Model;
using ContentManagementSystem.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using Ninject;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ContentManagementSystem.UI.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostManager _postManager;

        public PostController()
        {
            var kernel = new StandardKernel(new BllBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _postManager = kernel.Get<IPostManager>();
        }

        [System.Web.Http.Route("api/Posts/{status}/{offset}/{amount}")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Post> GetPageByStatus(string status, int offset, int amount)
        {
            return _postManager.GetPostsPaginationByStatus(offset, amount, status).Data;
        }

        [System.Web.Http.Route("api/Posts/{status}/TTotal/{tagId}")]
        [System.Web.Http.HttpGet]
        public int GetTotalCountByTag(string status, int tagId)
        {
            return _postManager.GetTotalPostCountByStatusAndTag(status, tagId).Data;
        }

        [System.Web.Http.Route("api/Posts/{status}/CTotal/{catId}")]
        [System.Web.Http.HttpGet]
        public int GetTotalCountByCat(string status, int catId)
        {
            var blah = _postManager.GetTotalPostCountByStatusAndCategory(status, catId).Data;
            return blah;
        }

        [System.Web.Http.Route("api/Posts/{status}/T/{tagId}/{offset}/{amount}")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Post> GetPageByTag(string status, int tagId, int offset, int amount)
        {
            return _postManager.GetPostsPaginationByStatusAndTag(offset, amount, status, tagId).Data;
        }

        [System.Web.Http.Route("api/Posts/{status}/C/{categoryId}/{offset}/{amount}")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Post> GetPageByCategory(string status, int categoryId, int offset, int amount)
        {
            return _postManager.GetPostsPaginationByStatusAndCategory(offset, amount, status, categoryId).Data;
        }

        [System.Web.Http.Route("api/ApprovedPosts/{status}")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Post> GetApprovedPosts(string status)
        {
            var r = new Response<List<Post>>();
            if (status == "Approved" && User.IsInRole("Staff"))
            {
                r = _postManager.GetAllByStatus(status, User.Identity.GetUserId());
            }
            else
            {
                r = _postManager.GetAllByStatus(status);
            }
            return r.Data;
        }

        [System.Web.Http.Route("api/Posts/{status}")]
        [System.Web.Http.HttpGet]
        // GET: api/Post changed id to status
        public IEnumerable<Post> GetPostsByStatus(string status)
        {
            var r = new Response<List<Post>>();
            if (status == "Draft" || status == "NeedsRevised")
            {
                r = _postManager.GetAllByStatus(status, User.Identity.GetUserId());
            }
            else
            {
                r = _postManager.GetAllByStatus(status);
            }
            //if (!r.Success)
            //{
            //    SetErrorTempData(r.Message);
            //}
            return r.Data;
        }

        // GET: api/Post/5
        public Post Get(int id)
        {
            return _postManager.Get(id).Data;
        }

        // POST: api/Post
        public HttpResponseMessage Post(PostCategoryTagVM viewModel) //(Post blogPost, string tags)//, string[] categories)//(JObject data)
        {
            Post blogPost = new Post()
            {
                Title = viewModel.Title,
                Author = new Author(){ Id = User.Identity.GetUserId(), Name = User.Identity.Name },
                CreatedDate = DateTime.Now,
                PublishDate = viewModel.PublishDate,
                ExpireDate = viewModel.ExpireDate,
                Content = viewModel.Content
            };
            Response<Post> response = _postManager.AddPost(blogPost, viewModel.TagList, viewModel.CategoryList, viewModel.Status);
            return Request.CreateResponse(HttpStatusCode.Created, response.Data);
        }

        //// PUT: api/Post/5
        public HttpResponseMessage Put(PostCategoryTagVM viewModel)
        {
            Post blogPost = new Post()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                PublishDate = viewModel.PublishDate,
                ExpireDate = viewModel.ExpireDate,
                Content = viewModel.Content
            };
            Response<Post> response = _postManager.UpdatePost(blogPost, viewModel.TagList, viewModel.CategoryList,viewModel.Status);
            return Request.CreateResponse(HttpStatusCode.Created, response.Data);
        }

        // DELETE: api/Post/5
        public void Delete(int id)
        {
            _postManager.DeletePost(id);
            //if (!r.Success)
            //{
            //    SetErrorTempData(r.Message);
            //}
        }
    }
}
