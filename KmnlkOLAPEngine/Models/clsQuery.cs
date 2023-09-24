using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPModel.Models
{
    //[DataContract] [DataMember(Name ="")]
    public class clsQuery 
    {
        public string source { set; get; }
        public ICollection<clsMeasure> measures { set; get; }
        public ICollection<clsDiminsion> diminsions { set; get; }
        public ICollection<clsCondition> conditions { set; get; }
    }
}
