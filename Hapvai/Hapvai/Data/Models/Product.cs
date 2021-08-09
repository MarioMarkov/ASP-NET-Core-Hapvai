using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        public Foodtype Foodtype { get; set; }

    }
}
