using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Data.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        public int Rating { get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IEnumerable<Order> Orders { get; init; } = new List<Order>();

    }
}
