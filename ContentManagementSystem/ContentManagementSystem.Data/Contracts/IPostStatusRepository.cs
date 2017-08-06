using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface IPostStatusRepository
    {
        List<PostStatus> GetAll();
        PostStatus GetById(string name);
    }
}
