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
    public class PostStatusRepository : IPostStatusRepository
    {
        public List<PostStatus> GetAll()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<PostStatus>("GetAllPostStatuses", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public PostStatus GetById(string name)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("status", name);
                PostStatus stat = cn.Query<PostStatus>("GetPostStatusByName", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return stat;
            }
        }
    }
}
