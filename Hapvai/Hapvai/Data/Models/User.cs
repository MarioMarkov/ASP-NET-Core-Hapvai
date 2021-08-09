using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Data.Models
{
    public class User: IdentityUser
    {
        [MaxLength(30)]
        public string FullName { get; set; }
    }
}
