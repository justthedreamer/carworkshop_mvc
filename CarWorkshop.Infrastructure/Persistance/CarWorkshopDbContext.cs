﻿using CarWorkshop.Application.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Persistance
{
    public class CarWorkshopDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; }
        public DbSet<Domain.Entities.CarWorkshopService> Services { get; set; }
        public DbSet<Domain.Entities.CarWorkshopRating> Ratings { get; set; }
        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options){ 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .OwnsOne(c => c.ContactDetails);

            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .HasMany(c => c.Services)
                .WithOne(s => s.CarWorkshop)
                .HasForeignKey(s => s.CarWorkshopId);

            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .HasMany(c => c.Ratings)
                .WithOne(s => s.CarWorkshop)
                .HasForeignKey(s => s.CarWorkshopId);
        }
    }
}
