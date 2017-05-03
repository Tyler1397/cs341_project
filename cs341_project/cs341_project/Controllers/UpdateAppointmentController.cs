// Author: Tyler Timm
// Description: Api for updating an appointment in the database
using cs341_project.Facades;
using cs341_project.Models;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class UpdateAppointmentController : ApiController
    {
        [Route("api/UpdateAppointment")]
        [HttpPost]
        public Appointment Post(AppointmentTemplate app)
        {
            DatabaseFacade facade = new DatabaseFacade();
            Appointment output = new Appointment();
            output.Patient = facade.GetPartialUser(app.Patient);
            output.Employee = facade.GetPartialUser(app.Employee);
            output.Status = "Changed";
            output.Title = app.Title;
            output.Time = app.Time;
            output.Date = app.Date;
            output.Id = app.Id;
            return facade.UpdateAppointment(output);

        }
    }
}
