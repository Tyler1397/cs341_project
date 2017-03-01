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
        public string Post(LoginCredentials login)
        {
            DatabaseFacade facade = new DatabaseFacade();
            return facade.GetUser(login);
        }
    }
}
