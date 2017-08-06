using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Data
{
    public static class Settings
    {
        public static string ConnectionString { get; private set; }

        static Settings()
        {
            ConnectionString = ConfigurationManager.AppSettings["Mode"] == "Live"
                ? ConfigurationManager.ConnectionStrings["JIST_Capstone"].ConnectionString
                : ConfigurationManager.ConnectionStrings["Test_JIST_Capstone"].ConnectionString;
        }
    }
}
