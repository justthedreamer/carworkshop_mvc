using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Mappings;
using CarWorkshop.Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAplication(this IServiceCollection service)
        {
            service.AddScoped<IUserContext, UserContext>();

            service.AddMediatR(typeof(CreateCarWorkshopCommand));

            service.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();

                cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            }).CreateMapper()
            );

                                                            service.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

        }

    }
}
