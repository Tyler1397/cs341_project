using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class Employee:User
    {
        public LinkedList<Appointment> FutureAppointments { get; set; }

        public LinkedList<Appointment> PastAppointments { get; set; }

        public string Role { get; set; }
    }
}