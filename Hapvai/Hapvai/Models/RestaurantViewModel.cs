using Hapvai.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Models
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Rating { get; set; }

        public string  Location { get; set; }

        public Category Category { get; set; }
    }
}
