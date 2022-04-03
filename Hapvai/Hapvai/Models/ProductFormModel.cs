using Hapvai.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Models
{
    public class ProductFormModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Foodtype")]
        public int FoodTypeId { get; init; }

        public int RestaurantId { get; set; }

        public IEnumerable<Foodtype> Foodtypes{ get; set; }
    }
}
