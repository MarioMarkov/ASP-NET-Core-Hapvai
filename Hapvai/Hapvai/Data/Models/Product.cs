using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Data.Models
{
    public class Product
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public double Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Name { get; set; }

        
        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }


        [ForeignKey("FoodtypeId")]
        public int FoodtypeId { get; set; }
        public Foodtype Foodtype { get; set; }


        
        public int? OrderId { get; set; }
        public Order Order { get; set; }

        

    }
}
