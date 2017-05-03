// Author: Tyler Timm
// Description: Model for appointment
using System;

namespace cs341_project.Models
{
    public class Appointment
    {
        public String Id { get; set; }

        public User Patient { get; set; }
        
        public User Employee { get; set; }
        
        public String Date { get; set; }
        
        public String Time { get; set; }
        
        public String Status { get; set; }
        
        public String Title { get; set; } 
    }
}