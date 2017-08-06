using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.BLL.Contracts
{
    public interface IPostManager
    {
        Response<int> GetTotalPostCountByStatusAndCategory(string status, int catId);
        Response<int> GetTotalPostCountByStatusAndTag(string status, int tagId);
        Response<List<Post>> GetPostsPaginationByStatusAndCategory(int offset, int maxAmount, string status, int categoryId);
        Response<List<Post>> GetPostsPaginationByStatusAndTag(int offset, int maxAmount, string status, int tagId);
        Response<List<Post>> GetPostsPaginationByStatus(int offset, int maxAmount, string status);
        Response<int> GetTotalPostCountByStatus(string status);
        Response<Post> Get(int id);
        Response<List<Post>> GetAllByStatus(string status, string userId = null);
        Response<Post> DeletePost(int id);
        Response<Post> AddPost(Post post, string[] tagArr, string[] catArr, string status);
        Response<Post> UpdatePost(Post post, string[] tagArr, string[] catArr, string status);
    }
}
