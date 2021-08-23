using Hapvai.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Models
{
    public class RestaurantShowModel
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();

    }
}
