using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.BLL.Contracts
{
    public interface IBlogManager
    {
        Response<Blog> Update(Blog b);
        Response<Blog> Get();
        Response<Blog> GetForIndex();
    }
}
