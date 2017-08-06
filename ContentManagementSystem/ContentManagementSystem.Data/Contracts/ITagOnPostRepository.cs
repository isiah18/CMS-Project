using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Data.Contracts
{
    public interface ITagOnPostRepository
    {
        void Add(int tagID, int postID);
    }
}
