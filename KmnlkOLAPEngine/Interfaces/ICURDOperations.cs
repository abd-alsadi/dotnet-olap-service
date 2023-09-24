using KmnlkOLAPModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPModel.Interfaces
{
     interface ICURDOperations
    {
        int Insert(clsQuery model);
        int Update(clsQuery model);
        int Delete(clsQuery model);
        T Get<T>(clsQuery model);
        T All<T>(clsQuery model);
        T Find<T>(int size,int page, clsQuery model);
    }
}
