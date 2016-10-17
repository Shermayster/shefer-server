using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Shefer.Models
{
    public class ActivityClass
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public string ProgramName { get; set; }
        public string ActivityType { get; set; }
        public string ProgramId { get; set; }
    }
}