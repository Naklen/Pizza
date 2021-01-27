using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models
{
    public class PizzaContext : DbContext
    {
        public DbSet<Product> Products { get; set; }        
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
