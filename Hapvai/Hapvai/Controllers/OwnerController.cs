using Hapvai.Data;
using Hapvai.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hapvai.Controllers
{
    public class OwnerController: Controller
    {
        private readonly ApplicationDbContext context;

        public OwnerController(ApplicationDbContext context)
        {
            this.context = context;
        }





        [Authorize]
        [HttpPost]
        public IActionResult Create() 
        {
            var userEmail = this.User.FindFirst(ClaimTypes.Email).Value;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var owner = new Owner() { Name=userEmail.Split("@")[0] ,UserId = userId };

            this.context.Owners.Add(owner);
            this.context.SaveChanges();

            return Redirect("/");
        } 
    }
}
