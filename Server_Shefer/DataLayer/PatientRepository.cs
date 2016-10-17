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
    public class PatientRepository
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = d:\\FinalProject\\shefer-server\\Server_Shefer\\App_Data\\Shefer_Data.accdb");
        //get patient
        public PatientClass FindPatien(int id)
        {
            var patient = this.db.Query<PatientClass>("SELECT * From Patients WHERE PatientId = @PatientId", new { PatientId = id }).SingleOrDefault();

            return patient;
        }
        //add patient
        public void AddPatient(PatientClass patient)
        {
            var sql = "INSERT INTO Patients([DoctorId], [Password]) VALUES(@DoctorId, @Password)";
            this.db.Query<string>(sql, patient);
        }

        //update patient data
        public void Update(int id, PatientClass patient)
        {
            var sql = "UPDATE Patients set [Password] = @Password where PatientId = @PatientId";
            this.db.Query<string>(sql, new { Password = patient.Password, PatientID = id });
        }

        //delete patient from database
        public void DeletePatient(int id)
        {
            var sql = "DELETE FROM Patients where PatientId = @PatientId";
            this.db.Query<string>(sql, new { PatientID = id });
        }
    }
}