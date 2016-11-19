using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.OleDb;
using System.Web.Http;
using Dapper;
using Server_Shefer.DataLayer;
using Server_Shefer.Models;

namespace Server_Shefer.Controllers
{
    public class DoctorController : ApiController
    {
        //private IDbConnection db =  new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
        //"Data Source = C:\\Final Project\\ServerSide\\Server_Shefer\\App_Data\\Shefer_Data.accdb");
        

        private DoctorsRepository _doctorsRepository;
        public DoctorController()
        {
            _doctorsRepository = new DoctorsRepository();
        }

        // GET: api/Doctor
        public IEnumerable<DoctorClass> Get()
        {
           return _doctorsRepository.GetAllDoctors();
        }

        // GET: api/Doctor/5
        public DoctorClass Get(int id)
        {   
          
           return _doctorsRepository.Find(id);
            
        }

        [Route("api/Email")]
        //Get by Email
        public HttpResponseMessage GetByEmail(string Email, string Password)
        {
            HttpResponseMessage response = null;
            var doctor = _doctorsRepository.FindByEmail(Email, Password);
            if (doctor == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, doctor);

            }
            return response;

            //var email = Email;   
            // return "hello";

        }

        // POST: api/Doctor - create
        [HttpPost]
        public void CreateDoctor(DoctorClass doctor)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
          
            _doctorsRepository.CreateDoctor(doctor);

        }

        // PUT: api/Doctor/5 - update
       // public void Put(int id, [FromBody]string value)
       [HttpPut]
        public void Put(int id, [FromBody] DoctorClass doctor)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

           
            _doctorsRepository.Update(id, doctor);
        }

        // DELETE: api/Doctor/5
        [HttpDelete]
        public void Delete(int id)
        {
            _doctorsRepository.DeleteDoctor(id);
        }
    }
}
