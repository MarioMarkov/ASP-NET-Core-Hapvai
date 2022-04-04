using Hapvai.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Models
{
    public class ProductEditModel
    {

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public int FoodtypeId { get; init; }
        public Foodtype Foodtype { get; set; }

        

        public IEnumerable<Foodtype> Foodtypes { get; set; }

    }
}
