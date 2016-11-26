using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using Dapper;
using Server_Shefer.Models;

namespace Server_Shefer.DataLayer
{
    public class PatientContactRepository
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data/Shefer_Data.accdb"));
        //get patient contact
        public PatientContact GetContact(int patientId)
        {
            var sql = "SELECT * From Contacts WHERE PatientId = @PatientId";
            return this.db.Query<PatientContact>(sql, new {PatientId = patientId}).SingleOrDefault();
        }

        //add patient contact
        public PatientContact AddContact(PatientContact contact)
        {
            var sql = "INSERT INTO Contacts ([PatientId], [ParentName], [LastName], [ChildName], [Tel], [Tel2], [Email]) VALUES (@PatientId, @ParentName, @ChildName, @LastName, @Tel, @Tel2, @Email)";
            var getContact = "SELECT * From Contacts WHERE PatientId = @PatientId";
            //delete all contacts
            var deleteContactSql = "DELETE * FROM Contacts  WHERE PatientId = @PatientId";
            this.db.Query<string>(deleteContactSql, new { PatientId = contact.PatientId });
            this.db.Query<PatientContact>(sql, new { PatientId = contact.PatientId,
                ParentName = contact.ParentName,
                LastName = contact.LastName,
                ChildName = contact.ChildName,
                Tel = contact.Tel,
                Tel2 = contact.Tel2,
                Email = contact.Email
            });
            return this.db.Query<PatientContact>(getContact, new { PatientId = contact.PatientId }).SingleOrDefault();
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