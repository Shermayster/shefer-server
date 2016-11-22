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
    public class PatientController : ApiController
    {
        private PatientRepository _patientRepository;
        private PatientContactRepository _patientContactRepository;
        public PatientController()
        {
            _patientRepository = new PatientRepository();
            _patientContactRepository = new PatientContactRepository();
        }
        // GET: api/Patient
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [Route("api/Family")]
        // GET: api/Patient/5
        public HttpResponseMessage Get(int password)
        {
            HttpResponseMessage response = null;
            var family = _patientRepository.FindPatien(password);
            response = family == null ? new HttpResponseMessage(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.OK, family);
            return response;
        }

        // get patient contact
        [Route("api/Patient/contact/{id}")]
        public PatientContact GetContact(int id)
        {
            return _patientContactRepository.GetContact(id);
        }

        //add contact to patient data
        [Route("api/Patient/contact")]
        [HttpPost]
        public void PostContact([FromBody] PatientContact contact)
        {
            _patientContactRepository.AddContact(contact);
        }

        // POST: api/Patient
        public PatientClass Post([FromBody]PatientClass patient)
        {
            return _patientRepository.AddPatient(patient);
        }

        // PUT: api/Patient/5
        public void Put(int id, [FromBody]PatientClass patient)
        {
            _patientRepository.Update(id, patient);
        }
        // PUT: api/Patient/5
        [Route("api/Patient/contact/update")]
        [HttpPut]
        public void UpdateAddress([FromBody]PatientContact contact)
        {
            _patientContactRepository.UpdateContact(contact);
        }

        // DELETE: api/Patient/5
        public void Delete(int id)
        {
            _patientRepository.DeletePatient(id);
        }
    }
}
