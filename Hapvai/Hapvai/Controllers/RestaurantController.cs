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


        public IActionResult All(string city) 
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
            if (!this.context.Restaurants.Any())
            {
                var data = new List<Restaurant>() {
                    new Restaurant(){ Name= "Luxor",Location="Sofia",CategoryId =1,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
                    new Restaurant(){ Name= "Happy",Location="Sofia",CategoryId =1,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
                    new Restaurant(){ Name= "Siluet",Location="Sofia",CategoryId =2,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
                    new Restaurant(){ Name= "Luxor",Location="Ruse",CategoryId =1,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
                    new Restaurant(){ Name= "Happy",Location="Ruse",CategoryId =1,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
                    new Restaurant(){ Name= "Siluet",Location="Ruse",CategoryId =2,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating=3,OwnerId=this.context.Owners.ToList()[0].Id}
                };
                this.context.Restaurants.AddRange(data);
                this.context.SaveChanges();
            }
            var restaurants = this.context.Restaurants.Select(r=> new RestaurantViewModel { 
                Name = r.Name,
                Category = r.Category.Name,
                Location = r.Location,
                Rating = r.Rating,
                ImageUrl = r.ImageUrl
            });

            restaurants = restaurants.Where(r => r.Location.ToLower() == city);

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
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var owner = this.context.Owners.FirstOrDefault(o => o.UserId == userId);
            

            var restaurant = new Restaurant()
            {
                Name = data.Name,
                Location = data.Location,
                OpenTime = data.OpenTime,
                CloseTime = data.CloseTime,
                ImageUrl = data.ImageUrl,
                CategoryId = data.CategoryId,
                OwnerId = owner.Id
            };

            this.context.Restaurants.Add(restaurant);
            this.context.SaveChanges();

            return Redirect("/");
        }


    }
}
