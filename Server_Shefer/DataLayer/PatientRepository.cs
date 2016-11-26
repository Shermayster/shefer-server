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
    public class PatientRepository
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data/Shefer_Data.accdb"));
        private ProgramRepository _programRepository = new ProgramRepository();
        private PatientContactRepository _patientContactRepository = new PatientContactRepository();

        //get patient
        public PatientClass FindPatien(int Password)
        {
            var patient = this.db.Query<PatientClass>("SELECT * From Patients WHERE Password = @Password", new { Password = Password }).SingleOrDefault();
            var patientId = patient.PatientID;
            patient.Program = _programRepository.GetProgram(patientId);
            patient.Contact = _patientContactRepository.GetContact(patientId);
            return patient;
        }
        //add patient
        public PatientClass AddPatient(PatientClass patient)
        {
            var sql = "INSERT INTO Patients([DoctorId], [Password]) VALUES(@DoctorId, @Password);";
            var sqlIdent =
                "SELECT * From Patients WHERE Password = @Password";
            this.db.Query<PatientContact>(sql, patient);
            var newPatient = db.Query<PatientClass>(sqlIdent, patient).FirstOrDefault();
            patient.Contact.PatientId = newPatient.PatientID;
            patient.Program[0].PatientId = newPatient.PatientID;
            newPatient.Contact = _patientContactRepository.AddContact(patient.Contact);
            newPatient.Program = new List<ProgramClass>();
            newPatient.Program.Add(_programRepository.CreateProgram(patient.Program[0]));
            return newPatient;    
        }

        //update patient data
        public void Update(PatientClass patient)
        {
            var sql = "UPDATE Patients set [Password] = @Password where PatientId = @PatientId";
            this.db.Query<string>(sql, new { Password = patient.Password, PatientID = patient.PatientID });
        }

        //delete patient from database
        public void DeletePatient(int id)
        {
            var sql = "DELETE FROM Patients where PatientId = @PatientId";
            this.db.Query<string>(sql, new { PatientID = id });
        }
    }
}