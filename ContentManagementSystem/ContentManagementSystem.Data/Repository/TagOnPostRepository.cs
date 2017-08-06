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
    public class TagOnPostRepository : ITagsOnPostsRepository
    {
        public void Add(int tagID, int postID)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("tagId", tagID);
                p.Add("postId", postID);
                cn.Execute("CreateTagOnPost", p, commandType: CommandType.StoredProcedure);
            }
        }

        public List<int> GetAllTagIdsForPost(int postId)
        {
            var p = new DynamicParameters();
            p.Add("@postId", postId, DbType.Int32);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<int>("SELECT TagId FROM TagsOnPosts WHERE PostId = @postId", p).ToList();
            }
        }

        public List<Tag> GetAllTagsForPost(int postId)
        {
            var p = new DynamicParameters();
            p.Add("@PostId", postId, DbType.Int32);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Tag>("SELECT t.* FROM TagsOnPosts tonp INNER JOIN Tags t ON tonp.TagId = t.Id WHERE PostId = @PostId", p).ToList();
            }
        }

        public int GetTotalCountById(int id)
        {
            var p = new DynamicParameters();
            p.Add("@TagId", id);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<int>("SELECT COUNT(PostId) FROM TagsOnPosts WHERE TagId = @TagId", p).First();
            }
        }

        public void Remove(int postId)
        {
            var p = new DynamicParameters();
            p.Add("@postId", postId, DbType.Int32);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute("DELETE FROM TagsOnPosts WHERE PostId = @postId", p);
            }
        }
    }
}
