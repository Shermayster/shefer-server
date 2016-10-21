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
    public class ProgramRepository
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = d:\\FinalProject\\shefer-server\\Server_Shefer\\App_Data\\Shefer_Data.accdb");
        //get program
        public ProgramClass GetProgram(int programId)
        {
            var sql = "SELECT * From Programs WHERE ProgramID = @ProgramID";
            var sqlActivities = "SELECT * FROM PatientActivities WHERE ProgramID = @ProgramID";
            var program =  this.db.Query<ProgramClass>(sql, new { ProgramID = programId }).SingleOrDefault();
            var activities = this.db.Query<PatientActivityClass>(sql, new {ProgramID = programId}).ToList();
            program.PatientActivityList = activities;
            return program;

        }
        //create program
        public void CreateProgram(ProgramClass program)
        {
            var sql = 
                "INSERT INTO Programs ([PatientId], [Status], [StartDay], [Duration], [CurrentWeek]) VALUES (@PatientId, @Status, @StartDay, @Duration, @CurrentWeek)";
            this.db.Query<string>(sql, new
            {
                PatientId = program.PatientId,
                Status = program.Status,
                StartDay = program.StartDay,
                Duration = program.Duration,
                CurrentWeek = program.CurrentWeek

            });
        }
        //update program
        public void UpdateProgram(ProgramClass program)
        {
            var sql = "UPDATE Programs set [PatientId] = @PatientId, [Status] = @Status, [StartDay] = @StartDay, [Duration]=@Duration, [CurrentWeek]=@CurrentWeek  WHERE ProgramID = @ProgramID";
            this.db.Query<string>(sql, new
            {
                PatientId = program.PatientId,
                Status = program.Status,
                StartDay = program.StartDay,
                Duration = program.Duration,
                CurrentWeek = program.CurrentWeek,
                ProgramID = program.ProgramID
            });
        }

        //delete program
        public void DeleteProgram(int programId)
        {
            var sql = "DELETE FROM Programs  WHERE ProgramID = @ProgramID";
            this.db.Query<string>(sql, new {ProgramID = programId});
        }
    }
}