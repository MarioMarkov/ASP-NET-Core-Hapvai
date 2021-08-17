using Hapvai.Data;
using Hapvai.Data.Models;
using Hapvai.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isUserOwner = this.context.Owners
                               .Any(d => d.UserId == userId);

            if (!isUserOwner) {

                return RedirectToAction("Create", "Owner");
            }

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
        [Authorize]
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
