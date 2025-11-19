using eCommerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            AppUser Admin = new()
            {
                FullName = "admin",
                Email = "admin1@gmail.com",
                UserName = "admin1@gmail.com",
                EmailConfirmed = true
            };
            if(!await userManager.Users.AnyAsync())
            {
                await userManager.CreateAsync(Admin , "Admin1@@@#");
                await userManager.AddToRoleAsync(Admin, "Admin");
            }
        }
    }
}
