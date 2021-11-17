using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizza.Components;
using Pizza.Models;
using Pizza.Utils;
using Pizza.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Controllers
{   
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        PizzaContext db;
        IWebHostEnvironment appEnvironment;
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public AdminController(PizzaContext context, IWebHostEnvironment appEnvironment, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            this.appEnvironment = appEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUserList([FromQuery] NewRoleSortViewModel model)
        {
            return ViewComponent(typeof (RolePanelUserListViewComponent), new {newSort = model.Sort});
        }

        [HttpPost]
        public async void ChangeUserRoles([FromBody] ChangeUserRoleViewModel model)
        {
            if (model.UserName == "admin" && model.RoleName == "admin")
                return;
            var user = await _userManager.FindByNameAsync(model.UserName);            
            if (user != null)
            {
                if (model.HasRole)
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }
        }

        public IActionResult ProductsList()
        {
            var list = db.Products.Select(p => new ProductViewModel {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Category = p.Category,
                    Description = p.Description,
                    Price = p.Price,
                    Image = new FormFile(null, 0, 0, null, p.ImagePath)
                }).ToList();
            return View(list);
        }

        public IActionResult ClearProductList()
        {
            foreach (var product in db.Products)
            {
                if (System.IO.File.Exists(appEnvironment.WebRootPath + product.ImagePath))
                    System.IO.File.Delete(appEnvironment.WebRootPath + product.ImagePath);
            }
            db.Products.RemoveRange(db.Products);
            db.SaveChanges();
            return RedirectToAction("ProductsList", "Admin");
        }

        [HttpPost]
        public IActionResult CreateOrEditProduct(ProductViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteProduct(ProductViewModel model)
        {
            var product = db.Products.Find(model.ProductId);
            if (product != null)
            {
                if (product.ImagePath != null)
                    if (System.IO.File.Exists(appEnvironment.WebRootPath + product.ImagePath))
                        System.IO.File.Delete(appEnvironment.WebRootPath + product.ImagePath);
                db.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("ProductsList", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> SaveChanging(ProductViewModel model)
        {
            var product = db.Products.Find(model.ProductId);
            if (product != null)
            {
                if (product.Name != model.Name)
                    product.Name = model.Name;
                if (product.Category != model.Category)
                    product.Category = model.Category;
                if (product.Description != model.Description)
                    product.Description = model.Description;
                if (product.Price != model.Price)
                    product.Price = model.Price;
                if (model.Image != null)
                {
                    var path = "/img/uploaded/" + model.Image.FileName;
                    using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                    if (product.ImagePath != null)                    
                        if (System.IO.File.Exists(appEnvironment.WebRootPath + product.ImagePath))                        
                            System.IO.File.Delete(appEnvironment.WebRootPath + product.ImagePath);                    
                    product.ImagePath = path;                    
                }
                else
                {
                    if (product.ImagePath != null)
                        if (System.IO.File.Exists(appEnvironment.WebRootPath + product.ImagePath))
                            System.IO.File.Delete(appEnvironment.WebRootPath + product.ImagePath);
                    product.ImagePath = null;                    
                }
                db.Products.Update(product);
            }
            else
            {
                product = new Product
                {
                    Name = model.Name,
                    Category = model.Category,
                    Description = model.Description,
                    Price = model.Price,
                    ImagePath = null
                };                
                if (model.Image != null)
                {
                    var path = "/img/uploaded/" + model.Image.FileName;
                    using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }                    
                    product.ImagePath = path;                    
                }
                db.Products.Add(product);                
            }
            db.SaveChanges();
            return RedirectToAction("ProductsList", "Admin");
        }
    }
}
