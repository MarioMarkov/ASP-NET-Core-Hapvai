using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Data.Models
{
    public class Owner
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public  string UserId { get; set; }


        public IEnumerable<Restaurant> Restaurants = new List<Restaurant>();
    }
}

