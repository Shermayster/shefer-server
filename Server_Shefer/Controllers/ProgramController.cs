using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Server_Shefer.DataLayer;
using Server_Shefer.Models;

namespace Server_Shefer.Controllers
{
    public class ProgramController : ApiController
    {
        private ProgramRepository _programRepository;
    
        public ProgramController()
        {
            _programRepository = new ProgramRepository();
        }
        // GET: api/Program
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Program/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Program
        public void Post([FromBody]ProgramClass program)
        {
            _programRepository.CreateProgram(program);
           
        }

        // PUT: api/Program/5
        public void Put(int id, [FromBody]ProgramClass program)
        {
            _programRepository.UpdateProgram(program);
        }
        [Route("api/feedback")]
        //Post: Add response to program
        public void Post([FromBody] ActivitiesResponse activitiesResponse)
        {
            _programRepository.CreateResponse(activitiesResponse);
        }
        [Route("api/updateActivity")]
        //Post: Add response to program
        public void Put([FromBody] PatientActivityClass activity)
        {
            _programRepository.UpdateActivity(activity);
        }
        // DELETE: api/Program/5
        public void Delete(int id)
        {
        }
    }
}
