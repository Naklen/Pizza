using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Components
{
    public class OrderPanelViewComponent : ViewComponent
    {
        PizzaContext _db;

        public OrderPanelViewComponent(PizzaContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            return View((_db.Orders.Where(o => !o.IsActive).ToList(), 
                _db.OrderItems.ToList(), 
                _db.OrderStatuses.ToList(), 
                _db.Products.ToList(),
                _db.Users.ToList()));
        }
    }
}
