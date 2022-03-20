using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Extensions
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration config)
        {

            var roles = new List<Role>
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"},
            };

            // creating roles if they don't exist
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            // create admin user if he doesn't exist
            var admin = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == "admin");
            if (admin == null)
            {
                admin = new User
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };

                var password = config.GetSection("Admin")["Password"];
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRolesAsync(admin, new[] { "Admin" });
            }

        }
    }
}