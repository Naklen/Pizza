﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string Id { get; set; }
        public User User { get; set; }
        
        public List<OrderItem> OrderItems { get; set; }
        public uint TotalPrice { get; set; }        
        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
