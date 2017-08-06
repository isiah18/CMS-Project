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
    public class ExceptionsRepository : IExceptionsRepository
    {
        public int Add(Exception ex)
        {
            var p = new DynamicParameters();
            p.Add("@DateAndTime", DateTime.Now, DbType.DateTime);
            p.Add("@Message", ex.Message, DbType.String);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Query("INSERT INTO Exceptions (DateAndTime, Message) VALUES (@DateAndTime, @Message) SET @Id = SCOPE_IDENTITY()", p);
            }
            return p.Get<int>("@Id");
        }

        public List<Exception> GetAll()
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Exception>("SELECT * FROM Exceptions").ToList();
            }
        }

        public void RemoveAll()
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Query("DELETE FROM Exceptions");
            }
        }
    }
}
