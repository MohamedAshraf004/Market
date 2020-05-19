using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Coupon> Coupons{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasOne(b => b.Brand).WithMany(p => p.Products).OnDelete(DeleteBehavior.NoAction);

        }

    }
}
