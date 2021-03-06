﻿// Author: Tyler Timm
// Description: Api for adding appointments to the database
using cs341_project.Facades;
using cs341_project.Models;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class AddAppointmentController : ApiController
    {
        [Route("api/AddAppointment")]
        [HttpPost]
        public Appointment Post(AppointmentTemplate app)
        {
            DatabaseFacade facade = new DatabaseFacade();
            Appointment output = new Models.Appointment();
            output.Patient = facade.GetPartialUser(app.Patient);
            output.Employee = facade.GetPartialUser(app.Employee);
            output.Status = app.Status;
            output.Title = app.Title;
            output.Time = app.Time;
            output.Date = app.Date;
            output.Status = app.Status;
            return facade.AddAppointment(output);
            
        }
    }
}
