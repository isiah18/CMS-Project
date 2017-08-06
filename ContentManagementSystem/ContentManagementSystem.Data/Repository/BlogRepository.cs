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
    public class BlogRepository : IBlogRepository
    {
        private readonly SqlConnection _cn;

        public BlogRepository()
        {
            _cn = new SqlConnection(Settings.ConnectionString);
        }

        public void Update(Blog b)
        {
            var p = new DynamicParameters();
            p.Add("@Title", b.Title, DbType.String);
            p.Add("@Tagline", b.TagLine, DbType.String);
            p.Add("@ContactDescription", b.ContactDescription, DbType.String);
            p.Add("@AdminId", 1, DbType.Int32);
            
            using (_cn)
            {
                _cn.Query("UPDATE Blog SET AdminId = @AdminId, Title = @Title, Tagline = @Tagline, ContactDescription = @ContactDescription WHERE AdminId = @AdminId", p);
            }
        }

        public Blog Get()
        {
            Blog b;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                b = cn.Query<Blog>("GetBlogData", commandType: CommandType.StoredProcedure).First();
            }

            return b;
        }
    }
}
