using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Data.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }


        [Required]
        public IEnumerable<OrderProduct> OrderProducts { get; init; }

        //[Required]
        //public Dictionary<Product,int> ProductsQuantities { get; init; } = new Dictionary<Product, int> ();
    }
}
