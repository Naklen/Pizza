using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
using Pizza.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        PizzaContext db;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, PizzaContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = db.Users.Find(userManager.GetUserId(User));
            var roles = await userManager.GetRolesAsync(user);
            return View((user, roles.Any(r => r =="admin")));
        }

        [Authorize]
        public IActionResult OrderList()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public void DeleteOrder([FromBody] DeleteOrderViewModel model)
        {            
            var order = db.Orders.Find(model.OrderId);
            if (order != null && order.IsClosed)
            {
                db.Orders.Remove(order);
            }
            db.SaveChanges();
        }

        [HttpGet]
        public IActionResult GetOrderListView()
        {
            return ViewComponent("OrderList");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {                
                User user = new User { Email = model.Email, UserName = model.UserName, Address = model.Address, PhoneNumber = model.PhoneNumber };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                var loginingUser = await userManager.FindByEmailAsync(model.Email);
                var userName = loginingUser == null ? "" : loginingUser.UserName;
                var result = await signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
