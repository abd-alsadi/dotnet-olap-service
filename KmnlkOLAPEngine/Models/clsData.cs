using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOLAPModel.Models
{
    //[DataContract] [DataMember(Name ="")]
    public class clsData:kmnlkOLAPModel
    {
        public ICollection<clsKey> keys { set; get; }
        public ICollection<clsMeasure> measures { set; get; }
    }
}
