using KmnlkOLAPModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPModel.Interfaces
{
    interface IValidationOperations
    {
        bool isValid(clsQuery model);
    }
}
