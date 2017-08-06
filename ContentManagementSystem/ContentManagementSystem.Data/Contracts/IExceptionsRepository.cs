using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface IExceptionsRepository
    {
        int Add(Exception ex);
        List<Exception> GetAll(); 
        void RemoveAll();
    }
}
