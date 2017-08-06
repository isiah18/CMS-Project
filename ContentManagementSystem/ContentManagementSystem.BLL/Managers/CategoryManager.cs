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
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IExceptionsRepository _exceptionsRepository;

        public CategoryManager()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _categoryRepository = kernel.Get<ICategoryRepository>();
            _exceptionsRepository = kernel.Get<IExceptionsRepository>();
        }

        public Response<Category> GetByName(string name)
        {
            var r = new Response<Category>();

            try
            {
                r.Success = true;
                r.Message = "Got category.";
                r.Data = _categoryRepository.GetByName(name);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to get category.";
                r.Data = new Category();
            }
            return r;
        }

        public Response<List<Category>> GetAll()
        {
            var response = new Response<List<Category>>();
            try
            {
                response.Data = _categoryRepository.GetAll();
                response.Success = true;
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
            }
            return response;
        }

        public Response<int> Add(string name)
        {
            var r = new Response<int>();

            try
            {
                r.Success = true;
                r.Message = "Added category.";
                r.Data = _categoryRepository.Add(name);

            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to add category.";
                r.Data = 0;
            }
            return r;
        }

        public Response<Category> Update(Category t)
        {
            var response = new Response<Category>();

            try
            {
                _categoryRepository.Update(t);
                response.Success = true;
                response.Message = "Updated category.";
                response.Data = t;
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to update category.";
                response.Data = t;
            }
            return response;
        }

        public Response<int> Remove(int id)
        {
            var response = new Response<int>();

            try
            {
                _categoryRepository.Remove(id);
                response.Success = true;
                response.Message = "Removed category.";
                response.Data = id;
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to remove category.";
                response.Data = id;
            }
            return response;
        }

        public Response<Category> GetById(int id)
        {
            var response = new Response<Category>();

            try
            {
                response.Success = true;
                response.Message = "Got category.";
                response.Data = _categoryRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                response.Success = false;
                response.Message = "Failed to get category.";
                response.Data = new Category();
            }
            return response;
        }
    }
}
