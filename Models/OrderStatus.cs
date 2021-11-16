using Pizza.Utils;
using System.ComponentModel.DataAnnotations;

namespace Pizza.Models
{
    public class OrderStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }
    }
}
