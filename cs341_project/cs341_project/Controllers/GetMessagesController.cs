// Author: Tyler Timm
// Description: Api for getting messages from the database, returns messages for a specified user
using cs341_project.Facades;
using cs341_project.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class GetMessagesController : ApiController
    {
        [Route("api/GetMessages")]
        [HttpPost]
        public LinkedList<Message> Post([FromBody]string id)
        {
            DatabaseFacade facade = new DatabaseFacade();
            return facade.GetMessages(id);
        }
    }
}
