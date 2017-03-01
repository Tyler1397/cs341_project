using cs341_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            if (login == null)
            {
                return null;
            }
            string user = "tyler1397";
            string password = "Password";

            if (user.Equals(login.Username, StringComparison.OrdinalIgnoreCase) && password.Equals(login.Password, StringComparison.Ordinal))
            {
                return "Welcome " + login.Username;
            }
            else
            {
                return "Error, username and/or password incorrect";
            }
        }
    }
}
