using Hapvai.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Models
{
    public class RestaurantShowModel
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();

        public IEnumerable<Foodtype> Foodtypes { get; init; } = new List<Foodtype>();

        public int Qunantity { get; set; }
    }
}
