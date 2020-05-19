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
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Coupon> Coupons{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed categories
            builder.Entity<Category>().HasData(new Category { Id = 1, Name = "Laptops" });
            builder.Entity<Category>().HasData(new Category { Id = 2, Name = "TVS" });
            builder.Entity<Category>().HasData(new Category { Id = 3, Name = "Phones" });

            ////Seed products
            builder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "HP ProBook",
                Price = 152.95,
                Description = "Awesome Laptop!",
                CategoryId = 1,
                Quantity = 6,
                Image = "HP.PNG",
                IsProductOfTheWeek = true
            });
            builder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Mac Book",
                Price = 252.95,
                Description = "Awesome Laptop!",
                CategoryId = 1,
                Quantity = 6,
                Image = "Mac.JPG",
                IsProductOfTheWeek = true
            }); builder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "IPhone11 Pro",
                Price = 175.95,
                Description = "Awesome Phone!",
                CategoryId = 3,
                Quantity = 3,
                Image = "Phone.JPG",
                IsProductOfTheWeek = true,
            });
            builder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Mac Tv",
                Price = 202.95,
                Description = "Awesome TV!",
                CategoryId = 2,
                Quantity = 6,
                Image = "TV.JPG",
                IsProductOfTheWeek = true
            });


        }

    }
}
