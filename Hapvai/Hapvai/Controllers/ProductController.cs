using Hapvai.Data;
using Hapvai.Data.Models;
using Hapvai.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult Add(int id)
        {

            if (!this.context.Foodtypes.Any())
            {
                var foodTypes = new List<Foodtype>() {
                    new Foodtype(){ Name ="Pizza"},
                    new Foodtype(){ Name ="Fish"},
                    new Foodtype(){ Name ="Mandja"},
                    new Foodtype(){ Name ="Meat"},
                    new Foodtype(){ Name ="Dessert"},

                };

                this.context.Foodtypes.AddRange(foodTypes);
                this.context.SaveChanges();
            }

            return View(new ProductFormModel { Foodtypes = this.context.Foodtypes });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(ProductFormModel data, int id)
        {

            var product = new Product()
            {
                Name = data.Name,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                FoodtypeId = data.FoodtypeId,
                RestaurantId = id
                
            };

            this.context.Products.Add(product);
            this.context.SaveChanges();

            return Redirect("/");
        }

        public IActionResult AllProductsForRestaurant(int restaurantId) 
        {
            var restaurant = this.context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);

            var products = this.context.Products.All(p => p.RestaurantId == restaurantId);
            ////var productsShow = new List<ProductShowModel>() { 
            ////products};
            return View(products);
        }

        public IActionResult Edit(int id)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == id);

            var productShow = new ProductEditModel 
            {
                ProductId = product.Id,
                Name = product.Name,
                FoodtypeId = product.FoodtypeId,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Foodtypes = this.context.Foodtypes
            };


            return View(productShow);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditModel data, int id)
        {
            var product = new Product()
            {
                
                Name = data.Name,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                FoodtypeId = data.FoodtypeId,
                RestaurantId = id

            };

            var productFromDb =  this.context.Products.First(p=> p.Id == id);
            productFromDb.Name = product.Name;
            productFromDb.Price = product.Price;
            productFromDb.ImageUrl = product.ImageUrl;
            productFromDb.FoodtypeId = product.FoodtypeId;

            await this.context.SaveChangesAsync();

            return Redirect($"/Restaurant/Show/{id}");
        }


    }
}
