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
    public class ProgramRepository
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data/Shefer_Data.accdb"));
        //get program
        public List<ProgramClass> GetProgram(int PatientID)
        {
            //Find patient program
            var sqlProgram = "SELECT * From Programs WHERE PatientId = @PatientId"; 
            //Find all activities in patients programs
            var sqlActivities = "SELECT * From PatientActivities WHERE ProgramId = @ProgramId AND PatientId = @PatientId";
            var programs =
                        this.db.Query<ProgramClass>(sqlProgram, new { PatientId = PatientID }).ToList();
            foreach (var program in programs)
            {
                program.PatientActivityList =
                    this.db.Query<PatientActivityClass>(sqlActivities,
                        new { ProgramId = program.ProgramID, PatientId = PatientID }).ToList();
            }
            
            return programs;

        }
        //create program
        public ProgramClass CreateProgram(ProgramClass program)
        {
            var sql = 
                "INSERT INTO Programs ([PatientId], [Status], [StartDay], [Duration], [CurrentWeek]) " +
                "VALUES (@PatientId, @Status, @StartDay, @Duration, @CurrentWeek)";
            var sqlId = "SELECT * From Programs WHERE PatientId = @PatientId";
            var sqlGetActivities = "SELECT * From PatientActivities WHERE ProgramID = @ProgramID";
            var  newProgram = this.db.Query<ProgramClass>(sql, new
            {
                PatientId = program.PatientId,
                Status = program.Status,
                StartDay = program.StartDay,
                Duration = program.Duration,
                CurrentWeek = program.CurrentWeek
            }).FirstOrDefault();
            var newP = this.db.Query<ProgramClass>(sqlId, new {PatientId = program.PatientId}).LastOrDefault();
            program.ProgramID = newP.ProgramID;
            // add new activities to the program
            var insertActivities = "INSERT INTO PatientActivities ([ProgramID], [ActivityId], [ActivityRestponce]," +
                               "[ActivityFeedback], [ActivityStatus],[ActivityName]," +
                               "[ActivityType], [ActivityGroupAge], [ActivityNameParent], [Description]) VALUES " +
                               " (@ProgramId, @ActivityId, @ActivityRestponce, @ActivityFeedback," +
                               "@ActivityStatus, @ActivityName, @ActivityType, @ActivityGroupAge, @ActivityNameParent, @Description)";
            foreach (var activity in program.PatientActivityList)
            {
                this.db.Query<string>(insertActivities, new
                {
                    ProgramID = newP.ProgramID,
                    ActivityId = activity.ActivityId,
                    ActivityRestponce = activity.ActivityRestponce,
                    ActivityFeedback = activity.ActivityFeedback,
                    ActivityStatus = activity.ActivityStatus,
                    ActivityName = activity.ActivityName,
                    ActivityType = activity.ActivityType,
                    ActivityGroupAge = activity.ActivityGroupAge,
                    ActivityNameParent = activity.ActivityNameParent,
                    Description = activity.Description
                });
            }
            newP.PatientActivityList = this.db.Query<PatientActivityClass>(sqlGetActivities, new { ProgramID = newP.ProgramID }).ToList();
            return newP;
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
            var deleteResponse = "DELETE * FROM ActivitiesResponse WHERE ProgramID = @ProgramID";
            this.db.Query<string>(deleteActivities, new {ProgramID = program.ProgramID});
            this.db.Query<string>(deleteResponse, new {ProgramID = program.ProgramID});
            // add new activities to the program
            var insertActivities = "INSERT INTO PatientActivities ([ProgramID], [ActivityId], [ActivityRestponce]," +
                               "[ActivityFeedback], [ActivityStatus],[ActivityName]," +
                               "[ActivityType], [ActivityGroupAge], [ActivityNameParent], [Description]) VALUES " +
                               " (@ProgramId, @ActivityId, @ActivityRestponce, @ActivityFeedback," +
                               "@ActivityStatus, @ActivityName, @ActivityType, @ActivityGroupAge, @ActivityNameParent, @Description)";
            foreach (var activity in program.PatientActivityList)
            {
                this.db.Query<string>(insertActivities, new
                {
                    ProgramID = activity.ProgramId,
                    ActivityId = activity.ActivityId, 
                    ActivityRestponce = activity.ActivityRestponce,
                    ActivityFeedback = activity.ActivityFeedback,
                    ActivityStatus = activity.ActivityStatus,        
                    ActivityName = activity.ActivityName,
                    ActivityType = activity.ActivityType,
                    ActivityGroupAge = activity.ActivityGroupAge,
                    ActivityNameParent = activity.ActivityNameParent,
                    Description = activity.Description
                });
            }
        }

        //delete program
        public void DeleteProgram(int programId)
        {
            var sql = "DELETE * FROM Programs  WHERE ProgramID = @ProgramID";
            this.db.Query<string>(sql, new {ProgramID = programId});
        }

        //add response to acivity
        public void CreateResponse(ActivitiesResponse activityResponse)
        {
            var sql =
                "INSERT INTO ActivitiesResponse ([ProgramID], [ActivityName], [ActivityResponse], [Week]) VALUES" +
                "(@ProgramID, @ActivityName, @ActivityResponse, @Week)";
            this.db.Query<ActivitiesResponse>(sql, new
            {
                ProgramID = activityResponse.ProgramID,
                ActivityName = activityResponse.ActivityName,
                ActivityResponse = activityResponse.ActivityResponse,
                Week = activityResponse.Week
            });
        }

        //update activity in program
        public void UpdateActivity(PatientActivityClass activity)
        {
            var sql =
                "UPDATE PatientActivities set [ActivityRestponce] = @ActivityRestponce WHERE PatienActivityId = @PatienActivityId";
            this.db.Query<PatientActivityClass>(sql, new
            {
                ActivityRestponce = activity.ActivityRestponce,
                PatienActivityId = activity.PatienActivityId
            });
        }
    }
}