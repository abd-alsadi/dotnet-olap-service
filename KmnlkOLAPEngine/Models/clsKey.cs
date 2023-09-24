using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPModel.Models
{
    //[DataContract] [DataMember(Name ="")]
    public class clsKey
    {
        public string name { set; get; }
        
        public string value { set; get; }
        public bool visible { set; get; }
        public bool isFilter { set; get; }
        public clsFilter filter { set; get; }
    }
}
