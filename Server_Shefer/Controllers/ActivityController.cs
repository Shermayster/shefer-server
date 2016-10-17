using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Server_Shefer.DataLayer;
using Server_Shefer.Models;

namespace Server_Shefer.Controllers
{
    public class ActivityController : ApiController
    {

        private ActivitiesDataRepository _activityDataRepository;

        public ActivityController()
        {
            _activityDataRepository = new ActivitiesDataRepository();
        }

        // GET: api/Activity

        public IEnumerable<ActivityClass> Get()
        {
            return _activityDataRepository.GetActivities();
        }

       
        // POST: api/Activity
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Activity/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Activity/5
        public void Delete(int id)
        {
        }
    }
}
