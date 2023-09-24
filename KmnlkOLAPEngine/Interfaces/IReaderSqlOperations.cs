using KmnlkOLAPModel.Models;
using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPModel.Interfaces
{
    public interface IReaderSqlOperations
    {
        T ReadSQL<T>(AdomdDataReader reader, clsQuery request);
        T ReadAllSQL<T>(AdomdDataReader reader, clsQuery request);
    }
}
