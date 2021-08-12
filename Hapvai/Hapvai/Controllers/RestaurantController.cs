using Hapvai.Data;
using Hapvai.Data.Models;
using Hapvai.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext context;

        public RestaurantController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //public IActionResult Order()
        //{
            
        //}


        public IActionResult All() 
        {
            var restaurants = this.context.Restaurants.Select(r=> new RestaurantViewModel { 
                Name = r.Name,
                Category = r.Category.Name,
                Location = r.Location,
                Rating = r.Rating,
                ImageUrl = r.ImageUrl
            });


            return View(restaurants);
        }

        public IActionResult Add()
        {
            if (!this.context.Categories.Any())
            {
                var categories = new List<Category>() {
                    new Category(){ Name ="Pizzaria"},
                    new Category(){ Name ="Meditarenian"},
                    new Category(){ Name ="Fish"},
                    new Category(){ Name ="Bulgarian"},
                    new Category(){ Name ="Chineese"},

                };

                this.context.Categories.AddRange(categories);
                this.context.SaveChanges();
            }

            return View(new RestaurantFormModel { Categories = this.context.Categories });
        }

        [HttpPost]
        public IActionResult Add(RestaurantFormModel data)
        {
            var restaurant = new Restaurant()
            {
                Name = data.Name,
                Location = data.Location,
                OpenTime = data.OpenTime,
                CloseTime = data.CloseTime,
                ImageUrl = data.ImageUrl,
                CategoryId = data.CategoryId,

            };

            this.context.Restaurants.Add(restaurant);
            this.context.SaveChanges();

            return Redirect("/");
        }


    }
}
