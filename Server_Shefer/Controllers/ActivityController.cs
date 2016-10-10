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

        private ActivitiesRepository _activityRepository;

        public ActivityController()
        {
            _activityRepository = new ActivitiesRepository();
        }

        // GET: api/Activity

        public IEnumerable<ActivityClass> Get()
        {
            return _activityRepository.GetActivities();
        }

        [Route("Activity/{prog}")]
        [HttpGet]
        // GET: api/Activity/5
        public List<ActivityClass> Get(string prog)
        {
            return _activityRepository.GetActivitiesByProgram(prog);
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
