﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
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

        public AdminController(PizzaContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            this.appEnvironment = appEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
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
