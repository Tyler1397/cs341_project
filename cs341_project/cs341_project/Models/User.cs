using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class User
    {
        public String Username { get; set; }

        public String Password { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Status { get; set; }

        public String Type { get; set; }

        public String StartDate { get; set; }

        public String EndDate { get; set; }

        public LinkedList<Message> Messages { get; set; }

        public LinkedList<Appointment> Appointments { get; set; }
    }
}