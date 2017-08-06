using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.BLL.Contracts
{
    public interface IPostStatusManager
    {
        Response<List<PostStatus>> GetAll();
    }
}
