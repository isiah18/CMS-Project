using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Data.Contracts
{
    public interface ICategoryOnPostRepository
    {
        void Add(int categoryID, int postID);
    }
}
