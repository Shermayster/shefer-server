using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Shefer.Models
{
    public class PatientContact
    {
        public int PatientId { get; set; }
        public string ParentName { get; set; }
        public string LastName { get; set; }
        public string ChildName { get; set; }
        public string Tel { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
    }
}