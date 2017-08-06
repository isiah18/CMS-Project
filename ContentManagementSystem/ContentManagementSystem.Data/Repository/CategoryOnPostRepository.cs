using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Model;
using Dapper;

namespace ContentManagementSystem.Data.Repository
{
    public class CategoryOnPostRepository : ICategoriesOnPostsRepository
    {
        public void Add(int categoryID, int postID)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("categoryId", categoryID);
                p.Add("postId", postID);
                cn.Execute("CreateCategoryOnPost", p, commandType: CommandType.StoredProcedure);
            }
        }

        public List<int> GetAllCategoryIdsForPost(int postId)
        {
            var p = new DynamicParameters();
            p.Add("@postId", postId, DbType.Int32);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<int>("SELECT CategoryId FROM CategoriesOnPosts WHERE PostId = @postId", p).ToList();
            }
        }

        public List<Category> GetAllCategoriesForPost(int postId)
        {
            var p = new DynamicParameters();
            p.Add("@PostId", postId, DbType.Int32);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var blah = cn.Query<Category>("SELECT c.* FROM CategoriesOnPosts cop INNER JOIN Categories c ON cop.CategoryId = c.Id WHERE PostId = @PostId", p).ToList();
                return blah;
            }
        }

        public void Remove(int postId)
        {
            var p = new DynamicParameters();
            p.Add("@postId", postId, DbType.Int32);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute("DELETE FROM CategoriesOnPosts WHERE PostId = @postId", p);
            }
        }
    }
}
