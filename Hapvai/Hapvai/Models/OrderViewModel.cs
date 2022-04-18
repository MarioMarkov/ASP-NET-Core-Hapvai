using Hapvai.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Models
{
    public class OrderViewModel
    {

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
        

    }
}
