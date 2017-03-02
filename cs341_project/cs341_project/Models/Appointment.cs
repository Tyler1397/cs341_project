using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class Appointment
    {
        public string Date { get; set; }

        public string Patient { get; set; }

        public string Employee { get; set; }

        public string Approved { get; set; }

        public string Cancelled { get; set; }

        public string Notes { get; set; }

        public string Time { get; set; }
    }
}