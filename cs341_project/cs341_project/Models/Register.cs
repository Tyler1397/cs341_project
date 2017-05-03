// Author: Tyler Timm
// Description: Model for registering for an account

namespace cs341_project.Models
{
    public class Register
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Status { get; set; }

        public string Role { get; set; }

        public string Type { get; set; }

        public string Date { get; set; }
    }
}