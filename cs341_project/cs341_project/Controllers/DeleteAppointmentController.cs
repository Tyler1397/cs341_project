using cs341_project.Facades;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class DeleteAppointmentController : ApiController
    {
        [Route("api/DeleteAppointment")]
        [HttpPost]
        public string Post([FromBody]string id)
        {
            DatabaseFacade facade = new DatabaseFacade();
            return facade.DeleteAppointment(id);
        }
    }
}
