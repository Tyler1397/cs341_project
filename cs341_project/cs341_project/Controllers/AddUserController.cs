using cs341_project.Facades;
using cs341_project.Models;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class AddUserController : ApiController
    {
        [Route("api/AddUser")]
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

            if (facade.UserExists(temp.Username))
            {
                return "Username " + app.Username + " already exists ";
            }

            string t = facade.AddUser(temp, app.Role);
            return "User: "+temp.Username+" successfully created " + t;
        
        }
    }
}
