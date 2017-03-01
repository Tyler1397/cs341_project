using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class Employee:User
    {
        public IEnumerable<Appointment> FutureAppointments { get; set; }

        public IEnumerable<Appointment> PastAppointments { get; set; }
    }
}