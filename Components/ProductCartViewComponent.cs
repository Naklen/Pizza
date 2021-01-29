using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Components
{
    public class ProductCartViewComponent : ViewComponent
    {        
        PizzaContext db;
        UserManager<User> userManager;

        public ProductCartViewComponent(PizzaContext context, UserManager<User> userManager)
        {
            db = context;
            this.userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {            
            if (User.Identity.Name != null)
            {
                var user = db.Users.Find(userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User));
                if (db.Orders.Any(o => o.User == user && o.IsActive))//user.Orders != null)                    
                        return View((db.Orders.Where(o => o.User == user && o.IsActive).FirstOrDefault(), db.Products, db.OrderItems));
            }
            Order nullOrder = null;            
            return View((nullOrder, db.Products, db.OrderItems));
        }
    }
}
