using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class Patient: User
    {
        public IEnumerable<Appointment> RequestedAppointments { get; set; }

        public IEnumerable<Appointment> FutureAppointments { get; set; }

        public IEnumerable<Appointment> PastAppointments { get; set; }

    }
}