// Author: Tyler Timm
// Description: Model to track User info after logging in
using System;

namespace cs341_project.Models
{
    public class LoginResult
    {
        public User User { get; set; }

        public Boolean Valid { get; set; }

        public String Message { get; set; }
    }
}