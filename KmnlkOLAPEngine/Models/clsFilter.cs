using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPModel.Models
{
    //[DataContract] [DataMember(Name ="")]
    public class clsFilter
    {
        public string operation { set; get; }
        
        public string value { set; get; }
    }
}
