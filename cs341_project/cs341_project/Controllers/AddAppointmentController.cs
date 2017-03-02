using cs341_project.Facades;
using cs341_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class AddAppointmentController : ApiController
    {
        [Route("api/AddAppointment")]
        [HttpPost]
        public Appointment Post(Appointment app)
        {
            DatabaseFacade facade = new DatabaseFacade();
            return facade.AddAppointment(app);
            
        }
    }
}
