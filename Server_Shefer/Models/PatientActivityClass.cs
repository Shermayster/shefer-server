using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Shefer.Models
{
    public class PatientActivityClass
    {
        public int PatienActivityId { get; set; }
        public string ActivityRestponce { get; set; }
        public string ActivityFeedback { get; set; }
        public string ActivityStatus { get; set; }
        public int ProgramId { get; set; }
        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityType { get; set; }
        public string RationaleCategory { get; set; }
        public string Description { get; set; }
        public string ActivityNameParent { get; set; }
        public int ActivityGroupAge { get; set; }
    }
}