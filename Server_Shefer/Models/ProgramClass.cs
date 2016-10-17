using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Shefer.Models
{
    public class ProgramClass
    {
        public int ProgramID { get; set; }
        public int PatientId { get; set; }
        public string Status { get; set; }
        public DateTime StartDay { get; set; }
        public int Duration { get; set; }
        public int CurrentWeek { get; set; }
        public List<PatientActivityClass> PatientActivityList { get; set; }
    }
}