using Hapvai.Data;
using Hapvai.Data.Models;
using Hapvai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            this.context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private static void Seed()
        {

            this.context.Categories.AddRange(
                 new List<Category> {
                     new Category() { Name = "Pizzaria" },
                    new Category() { Name = "Meditarenian" },
                    new Category() { Name = "Fish" },
                    new Category() { Name = "Bulgarian" },
                    new Category() { Name = "Chineese" }
                 }
            ) ;
            modelBuilder.Entity<Foodtype>().HasData(
                 new Foodtype { Name = "Salad" },
                    new Foodtype { Name = "Main Course" },
                    new Foodtype { Name = "Pasta" },
                    new Foodtype { Name = "Soup" },
                    new Foodtype { Name = "Dessert" }
            );
            var products = new List<Product>()
            {
                new Product { Name = "Musaka", FoodtypeId = 1, ImageUrl = "https://www.supichka.com/files/images/1242/musaka_2.jpg", Price = 5.99 },
                new Product { Name = "Shkembe", FoodtypeId = 1, ImageUrl = "https://i1.actualno.com/actualno_2013/upload/news/2015/05/13/0910631001431501146_1575116_920x708.webp", Price = 4.99 },
                new Product { Name = "Tarator", FoodtypeId = 1, ImageUrl = "https://recepti.gotvach.bg/files/lib/400x296/tarator-tikvichki.jpg", Price = 3.99 }
            };
            modelBuilder.Entity<Product>().HasData(products);

            modelBuilder.Entity<Restaurant>().HasData(new Restaurant()
            {
                Name = "Luxor",
                Location = "Sofia",
                CategoryId = 1,
                OpenTime = DateTime.Today.AddHours(10),
                CloseTime = DateTime.Today.AddHours(23),
                ImageUrl = "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                Rating = 3,
                OwnerId = 2
            }, new Restaurant()
            {
                Name = "Happy",
                Location = "Sofia",
                CategoryId = 1,
                OpenTime = DateTime.Today.AddHours(10),
                CloseTime = DateTime.Today.AddHours(23),
                ImageUrl = "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                Rating = 3,
                OwnerId = 2
            },
                    new Restaurant()
                    {
                        Name = "Siluet",
                        Location = "Sofia",
                        CategoryId = 2,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl = "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating = 3,
                        OwnerId = 2
                    },
                    new Restaurant()
                    {
                        Name = "Luxor",
                        Location = "Ruse",
                        CategoryId = 1,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl = "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating = 3,
                        OwnerId = 2
                    },
                    new Restaurant()
                    {
                        Name = "Happy",
                        Location = "Ruse",
                        CategoryId = 1,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl = "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating = 3,
                        OwnerId = 2
                    },
                    new Restaurant()
                    {
                        Name = "Siluet",
                        Location = "Ruse",
                        CategoryId = 2,
                        OpenTime = DateTime.Today.AddHours(10),
                        CloseTime = DateTime.Today.AddHours(23),
                        ImageUrl = "https://fastly.4sqi.net/img/general/600x600/3377828_7ARWIxzEHmWv3A2i3aZbGJE9-hHTgAeY33NzfAAvEUU.jpg",
                        Rating = 3,
                        OwnerId = 2
                    });
        }
    }
}
