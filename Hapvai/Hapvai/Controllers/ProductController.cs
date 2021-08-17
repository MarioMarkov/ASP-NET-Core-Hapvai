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
        public IActionResult Add()
        {
            if (!this.context.Foodtypes.Any()) {
                var foodTypes = new List<Foodtype>() {
                    new Foodtype {Name = "Salad"},
                    new Foodtype {Name = "Main Course"},
                    new Foodtype {Name = "Pasta"},
                    new Foodtype {Name = "Soup"},
                    new Foodtype {Name = "Dessert"}
                };

                this.context.Foodtypes.AddRange(foodTypes);
                this.context.SaveChanges();
            }

            return View(new ProductFormModel { Foodtypes = this.context.Foodtypes });
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
                FoodtypeId = data.FoodTypeId,
                
            };

            this.context.Products.Add(product);
            this.context.SaveChanges();

            return Redirect("/");
        }

    }
}
