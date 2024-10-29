using AutoMapper;
using CarWorkShop.Application.ApplicationUser;
using CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop;
using CarWorkShop.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IUserContext,UserContext>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateCarWorkshopCommandHandler>());

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new CarWorkShopMappingProfile(userContext));
            }).CreateMapper()
            );

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkShopCommandValidation>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
