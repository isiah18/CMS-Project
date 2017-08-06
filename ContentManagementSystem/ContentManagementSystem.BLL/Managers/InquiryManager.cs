using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.BLL.Contracts;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Data.NinjectBindings;
using ContentManagementSystem.Model;
using Ninject;

namespace ContentManagementSystem.BLL.Managers
{
    public class InquiryManager : IInquiryManager
    {
        private readonly IInquiryRepository _inquiryRepo;

        public InquiryManager()
        {
            var kernel = new StandardKernel(new DataBindings());
            kernel.Load(Assembly.GetExecutingAssembly());
            _inquiryRepo = kernel.Get<IInquiryRepository>();
        }

        public Response<List<Inquiry>> GetAllByStatus(string status)
        {
            var response = new Response<List<Inquiry>>();
            try
            {
                response.Data = _inquiryRepo.GetAll().Where(i => i.InquiryStatusId.ToString() == status).ToList();
                response.Success = true;
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }

        public Response<Inquiry> Get(int id)
        {
            var response = new Response<Inquiry>();
            try
            {
                response.Data = _inquiryRepo.Get(id);
                response.Success = true;
            }
            catch (Exception)
            {
                response.Data = new Inquiry();
                response.Success = false;
            }
            return response;
        }

      
        public Response<List<Inquiry>> Getall()
        {
            var response = new Response<List<Inquiry>>();
            try
            {
               response.Data = _inquiryRepo.GetAll();;
                response.Success = true;
            }
            catch (Exception)
            {
                response.Data = new List<Inquiry>();
                response.Success = false;
            }
            return response;
        }

        public Response<Inquiry> Add(Inquiry inquiry)
        {
            var response = new Response<Inquiry>();
            try
            {
                response.Data = new Inquiry();
               _inquiryRepo.Add(inquiry);
                response.Success = true;
            }
            catch (Exception)
            {
                response.Data = new Inquiry();
                response.Success = false;
            }
            return response;
        }

        public Response<Inquiry> Delete(int inquiryId)
        {
            var response = new Response<Inquiry>();
            try
            {
                response.Data = new Inquiry();
                _inquiryRepo.Delete(inquiryId);
                response.Success = true;
            }
            catch (Exception)
            {
                response.Data = new Inquiry();
                response.Success = false;
            }
            return response;
        }

        public Response<Inquiry> Update(Inquiry inquiry)
        {
            var response = new Response<Inquiry>();
            try
            {
                response.Data = new Inquiry();
                _inquiryRepo.UpdateStatus(inquiry);
                response.Success = true;
            }
            catch (Exception)
            {
                response.Data = new Inquiry();
                response.Success = false;
            }
            return response;
        }

        //public Response<Inquiry> MarkUnread(Inquiry inquiry)
        //{
        //    var response = new Response<Inquiry>();
        //    try
        //    {
        //        response.Data = new Inquiry();
        //        _inquiryRepo.UpdateStatus(inquiry);
        //        response.Success = true;
        //    }
        //    catch (Exception)
        //    {
        //        response.Data = new Inquiry();
        //        response.Success = false;
        //    }
        //    return response;
        //}

    }
}
