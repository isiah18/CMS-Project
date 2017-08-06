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
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetAll()
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Category>("GetAllCategories", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Category GetById(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Category>("SELECT * FROM Categories WHERE Id = @Id", p).FirstOrDefault();
            }
        }

        public Category GetByName(string name)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("categoryName", name);
                return cn.Query<Category>("GetCategoryByName", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public int Add(string name)
        {
            var p = new DynamicParameters();
            p.Add("@Name", name, DbType.String);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Query("INSERT INTO Categories (Name) VALUES (@Name) SET @Id = SCOPE_IDENTITY()", p);
            }

            return p.Get<int>("@Id");
        }

        public void Update(Category t)
        {
            var p = new DynamicParameters();
            p.Add("@Id", t.Id, DbType.Int32);
            p.Add("@Name", t.Name, DbType.String);

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Query("UPDATE Categories SET Name = @Name WHERE Id = @Id", p);
            }
        }

        public void Remove(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Query("DELETE FROM CategoriesOnPosts WHERE CategoryId = @Id", p);
                cn.Query("DELETE FROM Categories WHERE Id = @Id", p);
            }
        }
        
    }
}
