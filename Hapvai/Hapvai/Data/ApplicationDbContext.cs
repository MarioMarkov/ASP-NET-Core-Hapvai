using Hapvai.Data.Models;
using Hapvai.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hapvai.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Restaurant> Restaurants { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Order> Orders { get; init; }
        public DbSet<Product> Products { get; init; }
        public DbSet<Foodtype> Foodtypes { get; init; }
        public DbSet<Owner> Owners { get; init; }
       
        public DbSet<OrderItem> OrderItems { get; init; }

 

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            //problem
            builder
                 .Entity<Order>()
                 .HasOne(f => f.Restaurant)
                 .WithMany(p => p.Orders)
                 .HasForeignKey(p => p.RestaurantId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(f=> f.Foodtype)
                .WithMany(p=> p.Products)
                .HasForeignKey(p=> p.FoodtypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Restaurant>()
                .HasOne(c => c.Category)
                .WithMany(r => r.Restaurants)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                 .Entity<Restaurant>()
                 .HasMany(c => c.Orders)
                 .WithOne(r => r.Restaurant)
                 .HasForeignKey(c => c.RestaurantId)
                 .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<Owner>()
                .HasMany(o => o.Restaurants)
                .WithOne(r => r.Owner);

            builder
               .Entity<Owner>()
               .HasOne<IdentityUser>()
               .WithOne()
               .HasForeignKey<Owner>(d => d.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            
            base.OnModelCreating(builder);

            
        }

       

    }
}
