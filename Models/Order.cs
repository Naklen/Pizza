using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models
{
    public class Order : ModelObject
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        
        public List<OrderItem> OrderItems { get; set; }
        public uint TotalPrice { get; set; }       
    }
}
