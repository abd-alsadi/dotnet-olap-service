
using KmnlkOLAPModel;
using KmnlkOLAPModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkOLAPDll.Constants.Enums;

namespace KmnlkOLAPDll.Interfaces
{
     interface ICURDOperations
    {
        Enum_CURD_Result Insert(clsQuery model, ref string msgError);
        Enum_CURD_Result Update(clsQuery model, ref string msgError);
        Enum_CURD_Result Delete(clsQuery model, ref string msgError);
        object Get(clsQuery model, int type, ref string msgError);
        object All(clsQuery model, int type, ref string msgError);
        object Find(int size,int page, clsQuery model, int type, ref string msgError);
    }
}
