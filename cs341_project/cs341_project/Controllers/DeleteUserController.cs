using cs341_project.Facades;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class DeleteUserController : ApiController
    {
        [Route("api/DeleteUser")]
        [HttpPost]
        public string Post([FromBody]string id)
        {
            if(id == null)
            {
                return "id was null";
            }
            DatabaseFacade facade = new DatabaseFacade();
            return facade.DeleteUser(id);
        }
    }
}
