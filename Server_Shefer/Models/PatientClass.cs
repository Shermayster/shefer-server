using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Shefer.Models
{
    public class PatientClass
    {
        public int PatientID { get; set; }
        public int DoctorId { get; set; }
        public int Password { get; set; }
        public PatientContact Contact { get; set; }
        public List<ProgramClass> Program { get; set; }
    }
}