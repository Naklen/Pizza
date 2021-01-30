using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizza.Models;
using Pizza.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Controllers
{
    public class HomeController : Controller
    {
        PizzaContext db;
        private readonly UserManager<User> userManager;

        public HomeController(PizzaContext context, UserManager<User> userManager)
        {
            db = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpPost]
        public void AddToCart([FromBody] AddToCartViewModel model)
        {
            var newItem = new OrderItem { ProductId = model.ProductId, Product = db.Products.Find(model.ProductId), Quantity = model.Quantity };
            var user = db.Users.Find(userManager.GetUserId(User));
            var order = db.Orders.Where(o => o.IsActive && o.User == user).FirstOrDefault();
            order ??= new Order { User = user, OrderItems = new List<OrderItem>(), IsClosed = false, TotalPrice = 0, IsActive = true };
            //order.OrderItems.Exists(oi => oi.Product == newItem.Product)
            if (db.OrderItems.Any(oi => oi.Product == newItem.Product && oi.Order == order))
            {
                var item = db.OrderItems.Where(oi => oi.Product == newItem.Product && oi.Order == order).FirstOrDefault();
                item.Quantity = item.Quantity + newItem.Quantity;
                db.OrderItems.Update(item);
            }
            else
            {
                newItem.Order = order;
                db.OrderItems.Add(newItem);
                db.SaveChanges();
            }
            uint totalPrice = 0;
            foreach (var item in db.OrderItems.Where(oi => oi.Order == order))
            {
                totalPrice += (uint)(item.Quantity * db.Products.Find(item.ProductId).Price);
            }
            order.TotalPrice = totalPrice;
            if (db.Orders.Find(order.OrderId) == null)
            {
                db.Orders.Add(order);
            }
            else
            {
                db.Orders.Update(order);
            }
            db.SaveChanges();
        }

        [Authorize]
        [HttpPost]
        public void DeleteFromCart([FromBody] DeleteFromCartViewModel model)
        {
            var orderItem = db.OrderItems.Find(model.OrderItemId);
            var order = db.Orders.Find(orderItem.OrderId);
            order.TotalPrice -= (uint)(db.Products.Find(orderItem.ProductId).Price * orderItem.Quantity);
            db.OrderItems.Remove(orderItem);
            db.Orders.Update(order);
            if (!db.OrderItems.Any(oi => oi.OrderId == orderItem.OrderId))
            {
                db.Orders.Remove(order);
            }
            db.SaveChanges();
        }

        [Authorize]
        [HttpPost]
        public void MakeOrder()
        {
            var user = db.Users.Find(userManager.GetUserId(User));
            var order = db.Orders.FirstOrDefault(o => o.User == user && o.IsActive);
            if (order != null)
            {
                order.IsActive = false;
                order.IsClosed = true;
                db.Orders.Update(order);
                db.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult GetCartView()
        {           
            return ViewComponent("ProductCart");
        }
    }
}
