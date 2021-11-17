using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
using Pizza.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Controllers
{
    [Authorize(Roles = "manager")]
    public class ManagerController : Controller
    {
        PizzaContext _db;

        public ManagerController(PizzaContext db)
        {
            _db = db;
        }

        public IActionResult OrderPanel()
        {            
            return View();
        }

        [HttpPost]
        public void ChangeOrderStatus([FromBody] ChangeOrderStatusViewModel model)
        {
            var order = _db.Orders.Find(model.OrderId);
            if (order != null)
            {
                order.StatusId = (int)model.NewStatus;
                _db.Orders.Update(order);
            }
            _db.SaveChanges();
        }

        [HttpGet]
        public IActionResult GetOrderPanelView()
        {
            return ViewComponent("OrderPanel");
        }
    }
}
