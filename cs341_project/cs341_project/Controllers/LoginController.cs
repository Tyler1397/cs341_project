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
        public LoginResult Post(LoginCredentials login)
        {
            DatabaseFacade facade = new DatabaseFacade();
            LoginResult result = new LoginResult();
            facade.GetUser(login, result);
            switch (result.User.Type)
            {
                case "admin":
                    facade.SetupAdmin(result);
                    break;
                case "employee":
                    facade.SetupEmployee(result);
                    break;
                case "Patient":
                    facade.SetupPatient(result);
                    break;
            }
            return result;
        }
    }
}
