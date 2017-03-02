using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class Admin
    {
        public LinkedList<User> Users { get; set; }

        public LinkedList<Appointment> Appointments { get; set; }
    }
}