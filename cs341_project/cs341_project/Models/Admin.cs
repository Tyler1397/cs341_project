// Author: Tyler Timm
// Description: Model for User
using System.Collections.Generic;

namespace cs341_project.Models
{
    public class Admin : User
    {
        public LinkedList<User> Users { get; set; }
    }
}