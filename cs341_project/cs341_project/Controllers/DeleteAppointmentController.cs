// Author: Tyler Timm
// Description: Api for deleting appointments from the database
using cs341_project.Facades;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class DeleteAppointmentController : ApiController
    {
        [Route("api/DeleteAppointment")]
        [HttpPost]
        public void Post([FromBody]string id)
        {
            DatabaseFacade facade = new DatabaseFacade();
            facade.DeleteAppointment(id);
        }
    }
}
