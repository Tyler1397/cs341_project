// Author: Tyler Timm
// Description: Api for loggin into the web application 
using cs341_project.Facades;
using cs341_project.Models;
using System.Web.Http;
using System.Web.Routing;

namespace cs341_project.Controllers
{
    public class LoginController : ApiController
    {
        [Route("api/Login")]
        [HttpPost]
        public UserResult Post(LoginCredentials login)
        {
            DatabaseFacade facade = new DatabaseFacade();
            UserResult result = new UserResult();
            result = facade.GetUser(login);
            return result;
        }
    }
}
