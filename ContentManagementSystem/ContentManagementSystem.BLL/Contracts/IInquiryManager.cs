using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.BLL.Contracts
{
    public interface IInquiryManager
    {
        Response<List<Inquiry>> GetAllByStatus(string status);
        Response<Inquiry> Get(int id);
        Response<Inquiry> Add(Inquiry inquiry);
        Response<Inquiry> Delete(int inquiryId);
        Response<Inquiry> Update(Inquiry inquiry);
        //Response<Inquiry> MarkUnread(Inquiry inquiry);
        Response<List<Inquiry>> Getall();
    }
}
