using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models
{
    public class User : ModelObject
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; } 
        public string Email { get; set; }

        public List<Order> Orders { get; set; }
    }
}
