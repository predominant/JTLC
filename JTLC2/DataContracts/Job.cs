using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JTLC.DataContracts
{
    [DataContract]
    public class Job
    {
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name = "url")]
        public string Uri { get; set; }

        [DataMember(Name = "color")]
        public string Color { get; set; }
    }
}
