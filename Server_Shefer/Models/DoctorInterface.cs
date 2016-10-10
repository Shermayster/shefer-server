using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Shefer.Models
{
    interface DoctorInterface
    {
        DoctorClass Find(int id);
        List<DoctorClass> GetAll();
        DoctorClass Add(DoctorClass doctor);
        DoctorClass Update(DoctorClass doctor);
        void Remove(int id);
        DoctorClass GetFullContact(int id);
        void Save(DoctorClass doctor);
    }
}
