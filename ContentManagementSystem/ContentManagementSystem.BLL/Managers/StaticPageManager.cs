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
    public class StaticPageManager : IStaticPageManager
    {
        private readonly IStaticPageRepository _staticPageRepository;
        private readonly IExceptionsRepository _exceptionsRepository;

        public StaticPageManager()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _staticPageRepository = kernel.Get<IStaticPageRepository>();
            _exceptionsRepository = kernel.Get<IExceptionsRepository>();
        }

        public Response<int> Add(StaticPage sp)
        {
            var r = new Response<int>();

            try
            {
                r.Success = true;
                r.Message = "Added static page.";
                r.Data = _staticPageRepository.Add(sp);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to add static page.";
                r.Data = 0;
            }

            return r;
        }

        public Response<StaticPage> Update(StaticPage sp)
        {
            var r = new Response<StaticPage>();

            try
            {
                _staticPageRepository.Update(sp);
                r.Success = true;
                r.Message = "Updated static page.";
                r.Data = new StaticPage();
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to update static page.";
                r.Data = new StaticPage();
            }

            return r;
        }

        public Response<int> Remove(int id)
        {
            var r = new Response<int>();

            try
            {
                _staticPageRepository.Remove(id);
                r.Success = true;
                r.Message = "Removed static page.";
                r.Data = id;
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to remove static page.";
                r.Data = 0;
            }

            return r;
        }

        public Response<StaticPage> GetById(int id)
        {
            var r = new Response<StaticPage>();

            try
            {
                r.Success = true;
                r.Message = "Loaded static page.";
                r.Data = _staticPageRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load static page.";
                r.Data = new StaticPage();
            }

            return r;
        }

        public Response<StaticPage> GetByTitle(string title)
        {
            var r = new Response<StaticPage>();

            try
            {
                r.Success = true;
                r.Message = "Loaded static page.";
                r.Data = _staticPageRepository.GetByTitle(title);
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load static page.";
                r.Data = new StaticPage();
            }

            return r;
        }

        public Response<List<StaticPage>> GetAll()
        {
            var r = new Response<List<StaticPage>>();

            try
            {
                r.Success = true;
                r.Message = "Loaded all static pages.";
                r.Data = _staticPageRepository.GetAll();
            }
            catch (Exception ex)
            {
                _exceptionsRepository.Add(ex);
                r.Success = false;
                r.Message = "Failed to load all static pages.";
                r.Data = new List<StaticPage>();
            }

            return r;
        }
    }
}
