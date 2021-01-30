using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Components
{
    public class OrderListViewComponent : ViewComponent
    {
        PizzaContext db;
        UserManager<User> userManager;

        public OrderListViewComponent(PizzaContext context, UserManager<User> userManager)
        {
            db = context;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            List<Order> orders = null;
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User));
                orders = db.Orders.Where(o => o.User == user && o.IsClosed).ToList();
            }
            return View(orders);
        }
    }
}
