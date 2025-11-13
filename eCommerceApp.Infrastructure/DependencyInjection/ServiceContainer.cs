using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection Services , IConfiguration config)
        {
            Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default") ,
            sqloptions =>
            { //Ensure That this is The Correct Assembly
                sqloptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                sqloptions.EnableRetryOnFailure(); //Enable automatic retries for transient failures
            }) ,
            ServiceLifetime.Scoped );

            Services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            Services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();


            return Services;
        }
    }
}
