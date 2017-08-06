using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface ITagsOnPostsRepository
    {
        void Add(int tagID, int postID);
        List<int> GetAllTagIdsForPost(int postId);
        List<Tag> GetAllTagsForPost(int postId);
        int GetTotalCountById(int id);
        void Remove(int postId);
    }
}
