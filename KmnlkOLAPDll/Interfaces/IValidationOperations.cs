
using KmnlkOLAPModel;
using KmnlkOLAPModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPDll.Interfaces
{
    public interface IValidationOperations
    {
        bool isValid(clsQuery model);
    }
}
