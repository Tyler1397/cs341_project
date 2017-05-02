using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class LoginResult
    {
        public User User { get; set; }

        public Boolean Valid { get; set; }

        public String Message { get; set; }
    }
}