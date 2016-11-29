using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Shefer.Models
{
    public class ActivitiesResponse
    {
        public int ProgramID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityResponse { get; set; }
        public int Week { get; set; }
    }
}