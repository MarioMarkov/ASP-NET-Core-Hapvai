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
                .Entity<Menu>()
                .HasMany(m=> m.Products)
                .WithOne(m=> m.Menu)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

    }
}
