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
    public class TagManager : ITagManager
    {
        private readonly ITagRepository _tagRepository;
        private readonly IExceptionsRepository _exceptionsRepository;

        public TagManager()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _tagRepository = kernel.Get<ITagRepository>();
            _exceptionsRepository = kernel.Get<IExceptionsRepository>();
        }

        public Response<int> Add(string name)
        {
            var response = new Response<int>();

            try
            {
                response.Success = true;
                response.Message = "Added tag.";
                response.Data = _tagRepository.Add(name);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to add tag.";
                response.Data = 0;
            }
            return response;
        }

        public Response<Tag> Update(Tag t)
        {
            var response = new Response<Tag>();

            try
            {
                _tagRepository.Update(t);
                response.Success = true;
                response.Message = "Updated tag.";
                response.Data = t;
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to update tag.";
                response.Data = t;
            }
            return response;
        }

        public Response<int> Remove(int id)
        {
            var response = new Response<int>();

            try
            {
                _tagRepository.Remove(id);
                response.Success = true;
                response.Message = "Removed tag.";
                response.Data = id;
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to remove tag.";
                response.Data = id;
            }
            return response;
        }

        public Response<Tag> GetById(int id)
        {
            var response = new Response<Tag>();

            try
            {
                response.Success = true;
                response.Message = "Got tag.";
                response.Data = _tagRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to get tag.";
                response.Data = new Tag();
            }
            return response;
        }

        public Response<Tag> GetByName(string name)
        {
            var response = new Response<Tag>();

            try
            {
                response.Success = true;
                response.Message = "Got tag.";
                response.Data = _tagRepository.GetByName(name);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to get tag.";
                response.Data = new Tag();
            }
            return response;
        }

        public Response<List<Tag>> GetAll()
        {
            var response = new Response<List<Tag>>();

            try
            {
                response.Success = true;
                response.Message = "Got all tags.";
                response.Data = _tagRepository.GetAll();
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to get all tags.";
                response.Data = new List<Tag>();
            }
            return response;
        }
    }
}
