using Pizza.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.ViewModels
{
    public class ChangeOrderStatusViewModel
    {
        public int OrderId { get; set; }
        public OrderStatuses NewStatus { get; set; }
    }
}
