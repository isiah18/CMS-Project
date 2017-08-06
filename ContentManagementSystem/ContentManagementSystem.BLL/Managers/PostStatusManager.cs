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
    public class PostStatusManager : IPostStatusManager
    {
        private readonly IPostStatusRepository _postStatusRepository;

        public PostStatusManager()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _postStatusRepository = kernel.Get<IPostStatusRepository>();
        }

        public Response<List<PostStatus>> GetAll()
        {
            var response = new Response<List<PostStatus>>();
            try
            {
                response.Data = _postStatusRepository.GetAll();
                response.Success = true;
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }
    }
}
