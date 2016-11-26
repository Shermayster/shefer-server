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
    public class ActivitiesRepository:ActivityClass
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data/Shefer_Data.accdb"));

        public List<PatientActivityClass> GetActivities()
        {
           return this.db.Query<PatientActivityClass>("SELECT * FROM PatientActivities").ToList();

        }

        public List<PatientActivityClass> GetActivitiesByProgram(string program)
        {
            return this.db.Query<PatientActivityClass>("SELECT * FROM Activities WHERE ProgramName = '" + program + "'").ToList();
        }

    }
}