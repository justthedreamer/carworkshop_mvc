﻿using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistance;
using CarWorkshop.Infrastructure.Repositories;
using CarWorkshop.Infrastructure.Seenders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarWorkshopDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("CarWorkshop")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CarWorkshopDbContext>();

            services.AddScoped<CarWorkshopSeeder>();

            services.AddScoped<ICarWorkshopRepository, CarWorkshopRepository>();

            services.AddScoped<ICarWorkshopServiceRepository, CarWorkshopServiceRepository>();

            services.AddScoped<ICarWorkshopRatingRepository, CarworkshopRatingRepository>();

        }
    }
}
