using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface IPostRepository
    {
        int Add(Post p);
        void Update(Post p);
        void Remove(int id);
        Post Get(int id);
        List<Post> GetAll();
        int GetTotalPostCountByStatusAndTag(string status, int tagId);
        int GetTotalPostCountByStatusAndCategory(string status, int catId);
        int GetTotalPostCountByStatus(string status);
        List<Post> GetPostsPaginationByStatusAndCategory(int offset, int maxAmount, string status, int categoryId);
        List<Post> GetPostsPaginationByStatusAndTag(int offset, int maxAmount, string status, int tagId);
        List<Post> GetPostsPaginationByStatus(int offset, int maxAmount, string status);
        List<Post> Find(Expression<Func<Post, bool>> predicate);

        Author GetAuthorByPost(int postId);

        //todo implement pagination
        //List<Post> GetPage(int page, int max);
    }
}
