using cs341_project.Facades;
using cs341_project.Models;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class AddUserController : ApiController
    {
        [Route("api/AddAppointment")]
        [HttpPost]
        public string Post(Register app)
        {
            User temp = new Models.User();
            temp.FirstName = app.FirstName;
            temp.LastName = app.LastName;
            temp.Username = app.Username;
            temp.Password = app.Password;
            temp.Type = app.Type;
            DatabaseFacade facade = new DatabaseFacade();
            if (true)
            {
                return "Username " + app.Username + " already exists ";
            }
            return "ERROR";

        }
    }
}
