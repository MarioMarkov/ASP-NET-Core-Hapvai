using Hapvai.Data;
using Hapvai.Data.Models;
using Hapvai.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razor.Templating.Core;
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

            return View(new ProductFormModel { Foodtypes = this.context.Foodtypes,RestaurantId = id });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(ProductFormModel data)
        {

            var product = new Product()
            {
                Name = data.Name,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                FoodtypeId = data.FoodtypeId,
                RestaurantId = data.RestaurantId,


            };

            this.context.Products.Add(product);
            this.context.SaveChanges();

            return Redirect($"/Restaurant/Show/{data.RestaurantId}");
            //return Ok();
            //return Redirect($"/Restaurant/Show/{id}");
            //var products = this.context.Products.Include(p => p.Foodtype).Where(p => p.RestaurantId == id);

            //var restaurant = new RestaurantShowModel()
            //{
            //    RestaurantId = id,
            //    Name = data.Name,
            //    ImageUrl = data.ImageUrl,
            //    Products = products,
            //    Foodtypes = this.context.Foodtypes
            //};
            //var html = await RazorTemplateEngine.RenderAsync("~/Views/Product/_ViewAllProducts.cshtml", restaurant);

            //return Json(new { isValid = true });
        }

        public IActionResult AllProductsForRestaurant(int restaurantId)
        {
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
                var order = new Order() { RestaurantId = productFromDb.RestaurantId,OrderItems = new List<OrderItem> {  } };
                this.context.Orders.Add(order);
                await this.context.SaveChangesAsync();
                var orderFromDb = this.context.Orders.OrderBy(o => o.Id).LastOrDefault();

                var orderItem = new OrderItem 
                {
                    Product = productFromDb,
                    //Restaurant = productFromDb.Restaurant,
                    Quantity = 1,
                    OrderId = orderFromDb.Id
                };
                this.context.OrderItems.Add(orderItem);
                await this.context.SaveChangesAsync();
             
                HttpContext.Session.SetInt32(SessionOrderId, orderFromDb.Id);
               
            }
            else
            {
                var currentOrderId = HttpContext.Session.GetInt32(SessionOrderId);
                var order = this.context.Orders.Include(o=>o.OrderItems).FirstOrDefault(o => o.Id == currentOrderId);
                
                
                if (order.OrderItems.FirstOrDefault(oi => oi.ProductId == productFromDb.Id) != null)
                {
                    order.OrderItems.FirstOrDefault(oi => oi.ProductId == productFromDb.Id).Quantity++;
                    await this.context.SaveChangesAsync();
                }
                else 
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = productFromDb.Id,
                        Product = productFromDb,
                        OrderId = currentOrderId,
                        Quantity =1
                    };
                    order.OrderItems.Add(orderItem);
                    await this.context.SaveChangesAsync();
                }

            }


            return Redirect($"/Restaurant/Show/{productFromDb.RestaurantId}");
            //return Redirect($"/");
        }

        

    }
}
