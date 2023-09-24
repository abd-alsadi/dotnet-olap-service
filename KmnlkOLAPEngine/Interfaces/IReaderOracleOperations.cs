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
     public interface IReaderOracleOperations
    {
        T ReadORACLE<T>(AdomdDataReader reader, clsQuery request);
        T ReadAllORACLE<T>(AdomdDataReader reader, clsQuery request);
    }
}
