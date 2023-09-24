using KmnlkOLAPModel.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkCommon.Shareds.LoggerManagement;

namespace KmnlkOLAPModel.Connections
{
    public class ConnectionManager
    {
        protected string connectionString = "";
        protected string dataSource = ".";
        protected string database = "";
        protected string userID = "";
        protected string password = "";
        protected bool auth = false;
        protected int timeOut = 100000;
        protected Enums.DB_TYPE typeDB = Enums.DB_TYPE.SQL;
        protected ILog log;
        public string GetConnectionString()
        {
            return connectionString;
        }
        public Enums.DB_TYPE GetTypeDB()
        {
            return typeDB;
        }
        public int GetTimeOut()
        {
            return timeOut;
        }
        public virtual void execute(string query) { }
        public virtual void updateDB(string path) { }
        public virtual void createDB(string path) { }
    }
}
