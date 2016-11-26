using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Dapper;
using Server_Shefer.Models;

namespace Server_Shefer.DataLayer
{
    public class DoctorsRepository : DoctorClass
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data/Shefer_Data.accdb"));
        //get all doctors from database
        public List<DoctorClass> GetAllDoctors()
        {
            return this.db.Query<DoctorClass>("SELECT * FROM Doctors").ToList();
        }

        //get doctor by id
        public DoctorClass Find(int id)
        {
            var doctor = this.db.Query<DoctorClass>("SELECT * From Doctors WHERE DoctorId = @DoctorId", new { DoctorId = id }).SingleOrDefault();
            
            return doctor;
        }

       
        // get doctor by Email
        public DoctorClass FindByEmail(string Email, string Password)
        {
            //Find Doctor by Email
            var sql = "SELECT * From Doctors WHERE Email = @Email AND Password = @Password";
            //Find all patient from doctor
            var sqlPatient = "SELECT * From Patients WHERE DoctorId = @DoctorId";
            // Find patients contacts
            var sqlContact = "SELECT * From Contacts WHERE PatientId = @PatientId";
            //Find patient program
            var sqlProgram = "SELECT * From Programs WHERE PatientId = @PatientId";
            //Find all activities in patients programs
            var sqlActivities =
                "SELECT * From PatientActivities WHERE ProgramId = @ProgramId AND PatientId = @PatientId";
            //return doctor object
            var doctor = this.db.Query<DoctorClass>(sql, new { Email = Email, Password = Password}).SingleOrDefault();
            if (doctor != null)
            {
                var doctorId = doctor.DoctorId;


                //find patient data object
                var patientData = this.db.Query<PatientClass>(sqlPatient, new {DoctorId = doctorId}).ToList();
                // add contacts and program to patient object 
                foreach (var patient in patientData)
                {
                    patient.Contact =
                        this.db.Query<PatientContact>(sqlContact, new {PatientId = patient.PatientID}).FirstOrDefault();
                    patient.Program =
                        this.db.Query<ProgramClass>(sqlProgram, new {PatientId = patient.PatientID}).ToList();
                    foreach (var program in patient.Program)
                    {
                        program.PatientActivityList =
                            this.db.Query<PatientActivityClass>(sqlActivities,
                                new {ProgramId = program.ProgramID, PatientId = patient.PatientID}).ToList();
                    }
                }
                //add patient object to doctor object
                doctor.Patients = patientData;
                return doctor;
            }
            else
            {
                return null;
            }
        }


        //Add new Doctor
        public void CreateDoctor(DoctorClass doctor)
        {
            var sql = "INSERT INTO Doctors([Email], [Password]) VALUES(@Email, @Password)";
            this.db.Query<string>(sql, doctor);
        }

        //Change Doctors details
        public void Update(int id, DoctorClass doctor)
        {
            var sql = "UPDATE Doctors set Email = @Email, [Password] = @Password  where DoctorId = @DoctorId";
            this.db.Query<string>(sql, new { Email = doctor.Email, Password = doctor.Password, DoctorId = id });
        }

        //Delete Doctor from database
        public void DeleteDoctor(int id)
        {
            var sql = "DELETE FROM Doctors where DoctorId = @DoctorId";
            this.db.Query<string>(sql, new { DoctorId = id });
        }

    }
}