using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.BLL
{
    public static class IdentityDbSetting
    {
        public static string DbName { get; private set; }

        static IdentityDbSetting()
        {
            DbName = ConfigurationManager.AppSettings["Mode"] == "Live"
                ? "JIST_Capstone"
                : "Test_JIST_Capstone";
        }
    }
}
