using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class LoginResult
    {
        public Boolean Valid { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        public Patient Patient { get; set; }

        public Employee Employee { get; set; }

        public Admin Admin { get; set; }

        public string ErrorMessage { get; set; }

    }
}