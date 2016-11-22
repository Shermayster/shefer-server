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
    public class ActivitiesDataRepository
    {
        private IDbConnection db = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
                                                        "Data Source = " + HttpContext.Current.Server.MapPath("/App_Data/Shefer_Data.accdb"));

        public List<ActivityClass> GetActivities()
        {
            return this.db.Query<ActivityClass>("SELECT * FROM ActivitiesData").ToList();

        }
    }
}