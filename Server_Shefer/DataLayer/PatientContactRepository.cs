using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using Dapper;
using Server_Shefer.Models;

namespace Server_Shefer.DataLayer
{
    public class PatientContactRepository
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                         "Data Source = " + HttpContext.Current.Server.MapPath("/App_Data/Shefer_Data.accdb"));
        //get patient contact
        public PatientContact GetContact(int patientId)
        {
            var sql = "SELECT * From Contacts WHERE PatientId = @PatientId";
            return this.db.Query<PatientContact>(sql, new {PatientId = patientId}).SingleOrDefault();
        }

        //add patient contact
        public void AddContact(PatientContact contact)
        {
            var sql = "INSERT INTO Contacts ([PatientId], [ParentName], [LastName], [ChildName], [Tel], [Tel2], [Email]) VALUES (@PatientId, @ParentName, @ChildName, @LastName, @Tel, @Tel2, @Email)";
            this.db.Query<string>(sql, new { PatientId = contact.PatientId,
                ParentName = contact.ParentName,
                LastName = contact.LastName,
                ChildName = contact.ChildName,
                Tel = contact.Tel,
                Tel2 = contact.Tel2,
                Email = contact.Email
            });
        }
        //update contact
        public void UpdateContact(PatientContact contact)
        {
            var sql =
                "UPDATE Contacts set [PatientId] = @PatientId,  [ParentName]=@ParentName, [LastName]=@LastName, [ChildName]=@ChildName, [Tel]=@Tel, [Tel2]=@Tel2, [Email]=@Email where [PatientId] = @PatientId";
            this.db.Query<string>(sql, new {
                PatientId = contact.PatientId,
                ParentName = contact.ParentName,
                LastName = contact.LastName,
                ChildName = contact.ChildName,
                Tel = contact.Tel,
                Tel2 = contact.Tel2,
                Email = contact.Email
            });
        }
       
    }
}