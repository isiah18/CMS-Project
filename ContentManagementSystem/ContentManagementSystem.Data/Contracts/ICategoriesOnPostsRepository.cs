using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface ICategoriesOnPostsRepository
    {
        void Add(int categoryID, int postID);
        List<int> GetAllCategoryIdsForPost(int postId);
        List<Category> GetAllCategoriesForPost(int postId);

        void Remove(int postId);
    }
}
