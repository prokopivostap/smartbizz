using Microsoft.AspNetCore.Identity;
using SmartBiz.Application.DTO;
using SmartBiz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBiz.Infrastructure.DbSeed;

public static class DataSeeding
{
    public static async Task SeedRolesAndAdmin(UserManager<User> userManager, RoleManager<AppRole> roleManager)
    {
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new AppRole(role));
        }

        var admin = await userManager.FindByEmailAsync("admin@example.com");
        if (admin == null)
        {
            var newAdmin = new User { UserName = "admin", Email = "admin@example.com", FullName = "Administrator" };
            await userManager.CreateAsync(newAdmin, "Admin123!");
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}
