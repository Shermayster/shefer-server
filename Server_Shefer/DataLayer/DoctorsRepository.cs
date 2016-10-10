using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
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
                                                        "Data Source = C:\\Final Project\\ServerSide\\Server_Shefer\\App_Data\\Shefer_Data.accdb");

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
        //public string FindByEmail(string Email)
        {
            //return Email;

            var sql = "SELECT * From Doctors WHERE Email = @Email AND Password = @Password";
            var doctor = this.db.Query<DoctorClass>(sql, new { Email = Email, Password = Password}).SingleOrDefault();
            return doctor;
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