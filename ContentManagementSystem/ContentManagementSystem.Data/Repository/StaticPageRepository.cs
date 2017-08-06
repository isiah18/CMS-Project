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
    class StaticPageRepository : IStaticPageRepository
    {
        private SqlConnection _cn;

        private void NewCn()
        {
            _cn = new SqlConnection(Settings.ConnectionString);
        }

        public int Add(StaticPage sp)
        {
            var p = new DynamicParameters();
            p.Add("@IsLive", sp.IsLive, DbType.Boolean);
            p.Add("@Title", sp.Title, DbType.String);
            p.Add("@Content", sp.Content, DbType.String);
            p.Add("@ImagePath", sp.ImagePath, DbType.String);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            NewCn();
            using (_cn)
            {
                _cn.Query<int>("INSERT INTO [StaticPages] (IsLive, Title, Content, ImagePath) VALUES (@IsLive, @Title, @Content, @ImagePath) SET @Id = SCOPE_IDENTITY()", p);
            }
            return p.Get<int>("@Id");
        }

        public void Update(StaticPage sp)
        {
            var p = new DynamicParameters();
            p.Add("@Id", sp.Id, DbType.Int32);
            p.Add("@IsLive", sp.IsLive, DbType.Boolean);
            p.Add("@Title", sp.Title, DbType.String);
            p.Add("@Content", sp.Content, DbType.String);
            p.Add("@ImagePath", sp.ImagePath, DbType.String);
            NewCn();
            using (_cn)
            {
                _cn.Query("UPDATE [StaticPages] SET IsLive = @IsLive, Title = @Title, Content = @Content, ImagePath = @ImagePath WHERE Id = @Id", p);
            }
        }

        public void Remove(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            NewCn();
            using (_cn)
            {
                _cn.Query("DELETE FROM [StaticPages] WHERE Id = @Id", p);
            }
        }

        public StaticPage GetById(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            NewCn();
            using (_cn)
            {
                return _cn.Query<StaticPage>("SELECT * FROM [StaticPages] WHERE Id = @Id", p).Single();
            }
        }

        public StaticPage GetByTitle(string title)
        {
            var p = new DynamicParameters();
            p.Add("@Title", title);
            NewCn();
            using (_cn)
            {
                return _cn.Query<StaticPage>("SELECT * FROM [StaticPages] WHERE Title = @Title", p).Single();
            }
        }

        public List<StaticPage> GetAll()
        {
            NewCn();
            using (_cn)
            {
                return _cn.Query<StaticPage>("SELECT * FROM [StaticPages]").ToList();
            }
        }
    }
}
