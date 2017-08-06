using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Data;

namespace ContentManagementSystem.Test
{
    internal static class Utilities
    {
        internal static void RebuildTestDb()
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand
                {
                    CommandText = new FileInfo("RebuildTestDb.txt").OpenText().ReadToEnd(),
                    Connection = cn
                };

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
