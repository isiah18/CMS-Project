using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface IInquiryRepository
    {
        Inquiry Get(int Id);

        void Add(Inquiry inquiry);

        void Delete(int inquiryId);

        void UpdateStatus(Inquiry inquiry);

        void MarkAsUnread(Inquiry inquiry);

        List<Inquiry> GetAll();
    }
}
