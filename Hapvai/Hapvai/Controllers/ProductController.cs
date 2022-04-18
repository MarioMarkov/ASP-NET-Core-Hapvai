using Hapvai.Data;
using Hapvai.Data.Models;
using Hapvai.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        const string SessionOrderId = "_OrderId";

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

            return Redirect($"/Restaurant/Show/{id}");
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
            var restaurantId = this.context.Restaurants.FirstOrDefault(r => r.Id == product.RestaurantId).Id;
            var productShow = new ProductEditModel
            {
                ProductId = product.Id,
                Name = product.Name,
                FoodtypeId = product.FoodtypeId,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Foodtypes = this.context.Foodtypes,
                RestaurantId = restaurantId
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
                RestaurantId = data.RestaurantId

            };

            var productFromDb = this.context.Products.First(p => p.Id == id);
            productFromDb.Name = product.Name;
            productFromDb.Price = product.Price;
            productFromDb.ImageUrl = product.ImageUrl;
            productFromDb.FoodtypeId = product.FoodtypeId;


            await this.context.SaveChangesAsync();

            return Redirect($"/Restaurant/Show/{productFromDb.RestaurantId}");
        }

        //[HttpPost]
        public async Task<IActionResult> Order(int id)
        {
            var productFromDb = this.context.Products.FirstOrDefault(p => p.Id == id);

            if (HttpContext.Session.GetInt32(SessionOrderId) == null)
            {
                var order = new Order()
                {
                    RestaurantId = productFromDb.RestaurantId
                };
                
                order.Products.Append(productFromDb);
                

                this.context.Orders.Add(order);
                await this.context.SaveChangesAsync();
                var orderFromDb= this.context.Orders.OrderBy(o=>o.Id).LastOrDefault();
                var orderId = orderFromDb.Id;
                HttpContext.Session.SetInt32(SessionOrderId, orderId);
                
            }
            else 
            {
                var currentOrderId = HttpContext.Session.GetInt32(SessionOrderId);
                this.context.Orders.FirstOrDefault(o => o.Id == currentOrderId).Products.Append(productFromDb);
                await this.context.SaveChangesAsync();
            }


            return Redirect($"/Restaurant/Show/{productFromDb.RestaurantId}");
        }

    }
}
