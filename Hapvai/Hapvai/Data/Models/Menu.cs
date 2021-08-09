using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Data.Models
{
    public class Menu
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        [Required]
        public IEnumerable<Product> Products { get; init; } = new List<Product>();


    }
}
