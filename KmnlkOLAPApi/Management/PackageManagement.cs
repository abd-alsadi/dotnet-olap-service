
using KmnlkOLAPModel.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static KmnlkCommon.Shareds.LoggerManagement;

namespace KmnlkOLAPApi.Management
{
    public class PackageManagement
    {
        public ContextManagement context;
        public ILog logger;
        public PackageManagement()
        {
            string connectionString = SettingsManagement.getSetting(SettingsManagement.KEY_ConnectionString).ToString();
            string dbType = SettingsManagement.getSetting(SettingsManagement.KEY_DBType).ToString();

            string pathLog = SettingsManagement.getSetting(SettingsManagement.KEY_PathLog).ToString();
            string typeLog = SettingsManagement.getSetting(SettingsManagement.KEY_TypeLog).ToString();

            
           
            switch (typeLog.ToLower())
            {
                case "file":
                    logger = new FileLogger(pathLog);
                    break;
                case "db":
                    logger = new DBLogger(pathLog);
                    break;
                default:
                    logger = new FileLogger(pathLog);
                    break;
            }
            switch (dbType.ToLower())
            {
                case "sql":
                    context = new ContextManagement(new SqlConnectionManager(connectionString, logger), logger);
                    break;
                case "oracle":
                    context = new ContextManagement(new OracleConnectionManager(connectionString, logger), logger);
                    break;
                default:
                    context = new ContextManagement(new SqlConnectionManager(connectionString, logger), logger);
                    break;
            }



        }
    }
}