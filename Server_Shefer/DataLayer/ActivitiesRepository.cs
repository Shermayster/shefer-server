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
    public class ActivitiesRepository:ActivityClass
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = C:\\Final Project\\ServerSide\\Server_Shefer\\App_Data\\Shefer_Data.accdb");

        public List<ActivityClass> GetActivities()
        {
           return this.db.Query<ActivityClass>("SELECT * FROM Activities").ToList();

        }

        public List<ActivityClass> GetActivitiesByProgram(string program)
        {
            return this.db.Query<ActivityClass>("SELECT * FROM Activities WHERE ProgramName = '" + program + "'").ToList();
        }

    }
}