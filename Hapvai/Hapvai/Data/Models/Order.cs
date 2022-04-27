using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Data.Models
{
    public class Order
    {
        
        public int Id { get; set; }



        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();



        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
