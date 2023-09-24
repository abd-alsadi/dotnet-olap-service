using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPModel.Constants
{
    public static class PROCEDURES
    {
        //------------------------- Get -------------------------
      
        public static string QueryGetConstant = @"select $O {1} $C on columns  {2} from [{0}] {3} ";
        public static string QueryFilter = @"filter ([{0}].[{1}].children ,[{0}].[{1}].currentmember.memberValue  {2}  '{3}')";
        public static string QueryWhere = @"where ({0})";
        public static string QueryRows = @", $O nonempty({0}) $C on rows";
        public static string QueryMeasure =@"[Measures].[{0}] , ";


        public static string rowString ="\"{0}\":\"{1}\"";




    }
}
