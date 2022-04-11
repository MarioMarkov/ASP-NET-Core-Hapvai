using Hapvai.Data;
using Hapvai.Data.Models;
using Hapvai.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        
        public IActionResult Show(int id)
        {
            // data = this.context.Restaurants.FirstOrDefault(r => r.Id == id);

            //var products = new List<Product>() {
            //    new Product { Name = "Musaka",FoodtypeId=1,ImageUrl="https://www.supichka.com/files/images/1242/musaka_2.jpg",RestaurantId = id,Price=5.99},
            //    new Product { Name = "Musaka",FoodtypeId=2,ImageUrl="https://www.supichka.com/files/images/1242/musaka_2.jpg",RestaurantId = id,Price=5.99},
            //    new Product { Name = "Musaka",FoodtypeId=3,ImageUrl="https://www.supichka.com/files/images/1242/musaka_2.jpg",RestaurantId = id,Price=5.99}
            //};

            //if (!this.context.Products.Any()) 
            //{
            //    this.context.AddRange(products);
            //    this.context.SaveChanges();
            //}

            if (User.Identity.IsAuthenticated) 
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var isUserOwner = this.context.Owners
                               .Any(d => d.UserId == userId);
                if (isUserOwner)
                {
                    var owner = this.context.Owners.FirstOrDefault(o => o.UserId == userId);
                    bool showEdit = this.context.Restaurants.Any(r => id == owner.Id);

                    ViewData["showEdit"] = showEdit;
                }
                else { ViewData["showEdit"] = false; }
            }
            else { ViewData["showEdit"] = false; }

            var data = this.context.Restaurants.FirstOrDefault(r => r.Id == id);
            var products = this.context.Products.Where(p => p.RestaurantId == id);

            var restaurant = new RestaurantShowModel() { 
                RestaurantId = data.Id,
                Name= data.Name,
                ImageUrl = data.ImageUrl,
                Products = products,
                Foodtypes = this.context.Foodtypes
            };

            return View(restaurant);
        }


        public async Task<IActionResult> All(string city) 
        {

            //if (!this.context.Restaurants.Any())
            //{
            //    var data = new List<Restaurant>() {
            //        new Restaurant(){ Name= "Luxor",Location="Sofia",CategoryId =1,
            //            OpenTime = DateTime.Today.AddHours(10),
            //            CloseTime = DateTime.Today.AddHours(23),
            //            ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
            //            Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
            //        new Restaurant(){ Name= "Happy",Location="Sofia",CategoryId =1,
            //            OpenTime = DateTime.Today.AddHours(10),
            //            CloseTime = DateTime.Today.AddHours(23),
            //            ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
            //            Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
            //        new Restaurant(){ Name= "Siluet",Location="Sofia",CategoryId =2,
            //            OpenTime = DateTime.Today.AddHours(10),
            //            CloseTime = DateTime.Today.AddHours(23),
            //            ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
            //            Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
            //        new Restaurant(){ Name= "Luxor",Location="Ruse",CategoryId =1,
            //            OpenTime = DateTime.Today.AddHours(10),
            //            CloseTime = DateTime.Today.AddHours(23),
            //            ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
            //            Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
            //        new Restaurant(){ Name= "Happy",Location="Ruse",CategoryId =1,
            //            OpenTime = DateTime.Today.AddHours(10),
            //            CloseTime = DateTime.Today.AddHours(23),
            //            ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
            //            Rating=3,OwnerId=this.context.Owners.ToList()[0].Id},
            //        new Restaurant(){ Name= "Siluet",Location="Ruse",CategoryId =2,
            //            OpenTime = DateTime.Today.AddHours(10),
            //            CloseTime = DateTime.Today.AddHours(23),
            //            ImageUrl= "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
            //            Rating=3,OwnerId=this.context.Owners.ToList()[0].Id}
            //    };
            //    this.context.Restaurants.AddRange(data);
            //    this.context.SaveChanges();
            //}
            if (city == null || city == String.Empty || city == "") {
                var allRestaurants = this.context.Restaurants.Select(r => new RestaurantViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Category = r.Category.Name,
                    Location = r.Location,
                    Rating = r.Rating,
                    ImageUrl = r.ImageUrl
                });
                return View( await allRestaurants.ToListAsync());
            }
            var restaurants = this.context.Restaurants.Where(r => r.Location.ToLower() == city).Select(r=> new RestaurantViewModel { 
                Id = r.Id,
                Name = r.Name,
                Category = r.Category.Name,
                Location = r.Location,
                Rating = r.Rating,
                ImageUrl = r.ImageUrl
            });
            return View(await restaurants.ToListAsync());
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

        [Authorize]
        public async Task<IActionResult> AllRestaurantsOfOwner()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var owner = this.context.Owners.FirstOrDefault(o => o.UserId == userId);


            var restaurants = this.context.Restaurants.Where(r => r.OwnerId == owner.Id).Select(r => new RestaurantViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Category = r.Category.Name,
                Location = r.Location,
                Rating = r.Rating,
                ImageUrl = r.ImageUrl
            });

            return View("All", await restaurants.ToListAsync());
        }

        public async Task<IActionResult> AllRestaurantsFromSearch(string searchString)
        {
            var restaurants = from r in this.context.Restaurants
                              select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                restaurants = restaurants.Where(r => r.Name.Contains(searchString));
            }


            var restaurantsView = restaurants.Select(r => new RestaurantViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Category = r.Category.Name,
                Location = r.Location,
                Rating = r.Rating,
                ImageUrl = r.ImageUrl
            });

            return View("All", await restaurantsView.ToListAsync());
        }

       


    }
}
