using System;
using System.Collections.Generic;
using System.Text;
using Group3_Assignment2.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Group3_Assignment2.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Caribbean New Year",
                    Description = "Cruise the Caribbean & Celebrate the New Year",
                    Price = 4800.00m
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Polynesian Paradise",
                    Description = "8 Day All Inclusive Hawaiian Vacation",
                    Price = 3000.00m
                },
                new Product
                {
                    Id = 3,
                    ProductName = "Asian Expedition",
                    Description = "Airfaire, Hotel and Eco Tour",
                    Price = 2800.00m
                },
                new Product
                {
                    Id = 4,
                    ProductName = "European Vacation",
                    Description = "Euro Tour with Rail Pass and Travel Insurance",
                    Price = 3000.00m
                },
                new Product
                {
                    Id = 5,
                    ProductName = "Columbia Icefield Adventure",
                    Description = "A Remarkable Experience in the Canadian Rockies",
                    Price = 2000.00m
                },
                new Product
                {
                    Id = 6,
                    ProductName = "Mexico Mayan Adventure",
                    Description = "Explore incredible Mayan sites and colourful local markets",
                    Price = 2650.00m
                }
            );
        }
    }
}
