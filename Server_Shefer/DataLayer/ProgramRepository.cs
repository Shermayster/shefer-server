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
                "INSERT INTO Programs ([PatientId], [Status], [StartDay], [Duration], [CurrentWeek]) " +
                "VALUES (@PatientId, @Status, @StartDay, @Duration, @CurrentWeek)";
            var  newProgram = this.db.Query<ProgramClass>(sql, new
            {
                PatientId = program.PatientId,
                Status = program.Status,
                StartDay = program.StartDay,
                Duration = program.Duration,
                CurrentWeek = program.CurrentWeek
            }).SingleOrDefault();
            program.ProgramID = newProgram.ProgramID;
            // add new activities to the program
            var insertActivities = "INSERT INTO PatientActivities ([ProgramID], [ActivityId], [ActivityRestponce]," +
                               "[ActivityFeedback], [ActivityStatus],[ActivityName]," +
                               "[ActivityType], [ActivityGroup], [RationaleCategory], [Description]," +
                                   "[ActivityNameParent]) VALUES " +
                               " (@ProgramId, @ActivityId, @ActivityRestponce, @ActivityFeedback," +
                               "@ActivityStatus, @ActivityName, @ActivityType, @ActivityGroup" +
                                   "@ActivityGroup, @RationaleCategory, @Description, @ActivityNameParent)";
            foreach (var activity in program.PatientActivityList)
            {
                this.db.Query<string>(insertActivities, new
                {
                    ProgramID = activity.ProgramId,
                    ActivityId = activity.ActivityId,
                    ActivityRestponce = activity.ActivityResponce,
                    ActivityFeedback = activity.ActivityFeedback,
                    ActivityStatus = activity.ActivityStatus,
                    ActivityName = activity.ActivityName,
                    ActivityType = activity.ActivityType,
                    ActivityGroup = activity.ActivityGroup,
                    RationaleCategory = activity.RationaleCategory,
                    Description = activity.Description,
                    ActivityNameParent = activity.ActivityNameParent
                });
            }

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

            //delet all activities in the program
            var deleteActivities = "DELETE * FROM PatientActivities WHERE ProgramID = @ProgramID";
            this.db.Query<string>(deleteActivities, new {ProgramID = program.ProgramID});
            // add new activities to the program
            var insertActivities = "INSERT INTO PatientActivities ([ProgramID], [ActivityId], [ActivityRestponce]," +
                               "[ActivityFeedback], [ActivityStatus],[ActivityName]," +
                               "[ActivityType], [ActivityGroup]) VALUES " +
                               " (@ProgramId, @ActivityId, @ActivityRestponce, @ActivityFeedback," +
                               "@ActivityStatus, @ActivityName, @ActivityType, @ActivityGroup)";
            foreach (var activity in program.PatientActivityList)
            {
                this.db.Query<string>(insertActivities, new
                {
                    ProgramID = activity.ProgramId,
                    ActivityId = activity.ActivityId, 
                    ActivityRestponce = activity.ActivityResponce,
                    ActivityFeedback = activity.ActivityFeedback,
                    ActivityStatus = activity.ActivityStatus,        
                    ActivityName = activity.ActivityName,
                    ActivityType = activity.ActivityType,
                    ActivityGroup = activity.ActivityGroup
                });
            }
        }

        //delete program
        public void DeleteProgram(int programId)
        {
            var sql = "DELETE * FROM Programs  WHERE ProgramID = @ProgramID";
            this.db.Query<string>(sql, new {ProgramID = programId});
        }
    }
}