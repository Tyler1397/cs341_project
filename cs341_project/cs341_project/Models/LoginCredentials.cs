using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace cs341_project.Models
{
    public class LoginCredentials
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}