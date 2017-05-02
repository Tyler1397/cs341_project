using cs341_project.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cs341_project.Controllers
{
    public class DeleteMessageController : ApiController
    {
        [Route("api/DeleteMessage")]
        [HttpPost]
        public string Post([FromBody]string id)
        {
            DatabaseFacade facade = new DatabaseFacade();
            return facade.DeleteMessage(id);
        }
    }
}
