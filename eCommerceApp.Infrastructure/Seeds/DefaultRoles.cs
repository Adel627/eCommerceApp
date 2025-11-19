using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            IdentityRole Adminrole = new()  {Name = "Admin" };
            IdentityRole Userrole = new() {Name = "User" };
            
            if(!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(Adminrole);
                await roleManager.CreateAsync(Userrole);
            }
        }
    }
}
