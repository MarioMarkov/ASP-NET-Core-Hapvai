using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Data.Models
{
    public class Foodtype
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public IEnumerable<Product> Products{ get; init; } = new List<Product>();
    }
}
