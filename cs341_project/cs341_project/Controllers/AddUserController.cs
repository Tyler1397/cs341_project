// Author: Tyler Timm
// Description: Api for adding users to the database
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
            temp.StartDate = app.Date;
            temp.Status = app.Status;

            DatabaseFacade facade = new DatabaseFacade();

            if (facade.UserExists(temp.Username))
            {
                return "User " + temp.Username + " already exists"; ;
            }

            string t = facade.AddUser(temp,app.Role);
            return t;
        
        }
    }
}
