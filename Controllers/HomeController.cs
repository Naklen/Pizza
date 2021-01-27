using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizza.Models;
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

        public HomeController(PizzaContext context)
        {
            db = context;
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
    }
}
