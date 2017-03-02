using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class Patient: User
    {
        public LinkedList<Appointment> RequestedAppointments { get; set; }

        public LinkedList<Appointment> FutureAppointments { get; set; }

        public LinkedList<Appointment> PastAppointments { get; set; }

    }
}