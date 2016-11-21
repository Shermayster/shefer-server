using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Shefer.Models
{
    public class ActivityClass
    {
        public int ActivityID { get; set; }
        public int GroupAge { get; set; }
        public string RationaleCategory { get; set; }
        public string ActivityName { get; set; }
        public string ActivityNameParent { get; set; }
        public string Description { get; set; }
        public string ActivityType { get; set; }
    }
}