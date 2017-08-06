using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.BLL.Contracts
{
    public interface IStaticPageManager
    {
        Response<int> Add(StaticPage sp);
        Response<StaticPage> Update(StaticPage sp);
        Response<int> Remove(int id);
        Response<StaticPage> GetById(int id);
        Response<StaticPage> GetByTitle(string title);
        Response<List<StaticPage>> GetAll();
    }
}
