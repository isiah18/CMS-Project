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
    class TagRepository : ITagRepository
    {
        public int Add(string name)
        {
            var p = new DynamicParameters();
            p.Add("@Name", name, DbType.String);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Query("AddTag", p, commandType: CommandType.StoredProcedure);
            }

            return p.Get<int>("@Id");
        }

        public void Update(Tag t)
        {
            var p = new DynamicParameters();
            p.Add("@Id", t.Id, DbType.Int32);
            p.Add("@Name", t.Name, DbType.String);

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Query("UPDATE Tags SET Name = @Name WHERE Id = @Id", p);
            }
        }

        public void Remove(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Query("DELETE FROM [TagsOnPosts] WHERE TagId = @Id", p);
                cn.Query("DELETE FROM Tags WHERE Id = @Id", p);
            }
        }

        public Tag GetById(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Tag>("SELECT * FROM Tags WHERE Id = @Id", p).FirstOrDefault();
            }
        }

        public Tag GetByName(string name)
        {
            var p = new DynamicParameters();
            p.Add("@Name", name, DbType.String);

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Tag>("SELECT * FROM Tags WHERE Name = @Name", p).FirstOrDefault();
            }
        }

        public List<Tag> GetAll()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Tag>("SELECT * FROM Tags").ToList();
            }
        }
    }
}
