using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.BLL.Contracts
{
    public interface ITagManager
    {
        Response<int> Add(string name);
        Response<Tag> Update(Tag t);
        Response<int> Remove(int id);
        Response<Tag> GetById(int id);
        Response<Tag> GetByName(string name);
        Response<List<Tag>> GetAll();
    }
}
