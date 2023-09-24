using KmnlkOLAPModel.Connections;
using KmnlkOLAPModel.Managers;
using KmnlkOLAPModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkCommon.Shareds.LoggerManagement;

namespace MyTest
{
    class Program
    {
        static void Main(string[] args)
        {
           string connectionString = @"Provider=MSOLAP; Data Source=.; Initial Catalog =test1";
            ILog logger = new FileLogger(@"E:\my projects\KmnlkOLAP\KmnlkOLAPApi\Output\Log\Local\");
            ConnectionManager conn = new SqlConnectionManager(connectionString, logger);

            OlapManager manager = new OlapManager(conn, logger);
            clsQuery query = new clsQuery();
            query.source = "Pubs";
            query.measures = new List<clsMeasure>();
            query.measures.Add(new clsMeasure() {name="Sales Count"});
            List<clsQuery> xxx=manager.Get<List<clsQuery>>(query);
            Console.Write(xxx);
            Console.ReadLine();
        }
    }
}
