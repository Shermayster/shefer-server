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
        private ProgramRepository _programRepository = new ProgramRepository();
        private PatientContactRepository _patientContactRepository = new PatientContactRepository();

        //get patient
        public PatientClass FindPatien(int id)
        {
            var patient = this.db.Query<PatientClass>("SELECT * From Patients WHERE PatientId = @PatientId", new { PatientId = id }).SingleOrDefault();

            return patient;
        }
        //add patient
        public PatientClass AddPatient(PatientClass patient)
        {
            var sql = "INSERT INTO Patients([DoctorId], [Password]) VALUES(@DoctorId, @Password);";
            var sqlIdent =
                "SELECT * From Patients WHERE Password = @Password";
            this.db.Execute(sql, patient);
            var newPatientID = db.Query<PatientClass>(sqlIdent, patient).FirstOrDefault();
            patient.Contact.PatientId = newPatientID.PatientID;
            patient.Program[0].PatientId = newPatientID.PatientID;
            _patientContactRepository.AddContact(patient.Contact);
            _programRepository.CreateProgram(patient.Program[0]);
            return newPatientID;
           
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