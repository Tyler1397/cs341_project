using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cs341_project.Models
{
    public class AppointmentTemplate
    {
        public String Patient { get; set; }

        public String Employee { get; set; }

        public String Date { get; set; }

        public String Time { get; set; }

        public String Status { get; set; }

        public String Title { get; set; }
    }
}