// Author: Tyler Timm
// Description: Model template for an appointment, used by the front end
using System;

namespace cs341_project.Models
{
    public class AppointmentTemplate
    {
        public String Id { get; set; }

        public String Patient { get; set; }

        public String Employee { get; set; }

        public String Date { get; set; }

        public String Time { get; set; }

        public String Status { get; set; }

        public String Title { get; set; }
    }
}