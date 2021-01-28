using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models
{
    public class User : IdentityUser
    {
        public string Address { get; set; }
        public List<Order> Orders { get; set; }
    }
}
