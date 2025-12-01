using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Middelwares;
using eCommerceApp.Infrastructure.Repositories;
using eCommerceApp.Infrastructure.Repositories.Authentication;
using eCommerceApp.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
                sqloptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName); // put migrations folder in the project where the appdbcontext in .
                sqloptions.EnableRetryOnFailure();  //Enable automatic retries for transient failures
            }).UseExceptionProcessor() ,  //To Enable make exceptions of Db (Convert exceptions of DB to Known Exceptions)
            ServiceLifetime.Scoped );

           
            Services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));
            Services.AddScoped<IRoleManagement , RoleManagement>();
            Services.AddScoped<IUserManagement , UserManagement>();
            Services.AddScoped<ITokenManagement , TokenManagement>();
            Services.AddScoped<IPaymentService , StripePaymentService>();
         

            Services.AddScoped<ICategoryRepository , CategoryRepository>();
            Services.AddScoped<IProductRepository , ProductRepository>();
            Services.AddScoped<IRateRepository, RateRepository>();
            Services.AddScoped<ICommentRepository, CommentRepository>();
            Services.AddScoped<ICartRepository, CartRepository>();
            Services.AddScoped<ICartIemsRepository, CartItemRepository>();


            //Stripe configurations

            Stripe.StripeConfiguration.ApiKey = config["Stripe:SecretKey"];


            Services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();


            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidAudience = config["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                };
                // if there any proplem in token and you want to debug to know the reason
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        Console.WriteLine("Auth header: " + ctx.Request.Headers["Authorization"].ToString());
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine("Auth failed: " + ctx.Exception?.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = ctx =>
                    {
                        Console.WriteLine("Token validated for: " + ctx.Principal?.Identity?.Name);
                        return Task.CompletedTask;
                    }
                };

            });
            return Services;
        }

        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app) 
        {
          app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
