using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrashCollector.Models;

namespace TrashCollector.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Day> Days { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                }
            );
            
            builder.Entity<Day>().HasData(
                new Day
                {
                    DayId = 1,
                    DayOfWeek = "Sunday"
                },
                new Day
                {
                    DayId = 2,
                    DayOfWeek = "Monday"
                },
                new Day
                {
                    DayId = 3,
                    DayOfWeek = "Tuesday"
                },
                new Day
                {
                    DayId = 4,
                    DayOfWeek = "Wednesday"
                },
                new Day
                {
                    DayId = 5,
                    DayOfWeek = "Thursday"
                },
                new Day
                {
                    DayId = 6,
                    DayOfWeek = "Friday"
                },
                new Day
                {
                    DayId = 7,
                    DayOfWeek = "Saturday"
                }             
            );
        }
    }
}
