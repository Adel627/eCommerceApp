
using eCommerceApp.Application.DependencyInjection;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Infrastructure.DependencyInjection;
using eCommerceApp.Infrastructure.Middelwares;
using eCommerceApp.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace eCommerceApp.Host
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();
            Log.Logger.Information("Application is building.......");

            // Add services to the container.
            builder.Services.AddApplicationService();
            builder.Services.AddInfrastructureService(builder.Configuration);
           
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>

                policy.AllowAnyOrigin().AllowAnyHeader()
                .AllowAnyMethod().AllowCredentials()
                );
            });

            try
            {
                var app = builder.Build();

                app.UseSerilogRequestLogging();
               

                app.UseInfrastructureService();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                //Add Seeding
                var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
                using var scope = scopeFactory.CreateScope();

                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                await DefaultRoles.SeedAsync(roleManger);
                await DefaultUsers.SeedAsync(userManger);


                app.MapControllers();
                Log.Logger.Information("Application is running.......");



                app.Run();
            }
            catch (Exception ex) 
            {

                Log.Logger.Error(ex , "Application failed to start.......");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
