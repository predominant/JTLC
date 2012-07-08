using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JTLC.DataContracts
{
    [DataContract]
    public class JenkinsResponse
    {
        [DataMember(Name="jobs")]
        public Job[] Jobs { get; set; }
    }
}
