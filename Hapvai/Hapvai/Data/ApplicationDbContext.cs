using Hapvai.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hapvai.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Restaurant> Restaurants { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Menu> Menus { get; init; }
        public DbSet<Order> Orders { get; init; }
        public DbSet<Product> Products { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Restaurant>()
                .HasOne(c => c.Category)
                .WithMany(r => r.Restaurants)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Restaurant>()
                .HasOne(r => r.Menu)
                .WithOne(r => r.Restaurant)
                .HasForeignKey<Menu>(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Order>()
                .HasMany(o => o.Products);

            builder
                 .Entity<Product>()
                 .HasOne(p => p.Menu)
                 .WithMany(p => p.Products)
                 .HasForeignKey(p=> p.MenuId);

            builder
                 .Entity<Product>()
                 .HasOne(p => p.Order)
                 .WithMany(p => p.Products)
                 .HasForeignKey(p=> p.OrderId);

            base.OnModelCreating(builder);
        }

    }
}
