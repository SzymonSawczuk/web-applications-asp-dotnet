using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using lab13.Model;
using System.Threading.Tasks;

namespace lab13.Data
{
    public class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        // name - poprawny adres email
        // password - min 8 znaków, mała i duża litera, cyfra i znak specjalny
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin",
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("User").Result)
{
                 IdentityRole role = new IdentityRole
                 {
                     Name = "User",
                 };
                 IdentityResult roleResult = roleManager.CreateAsync(role).Result;
             }
            
        }

        public static void SeedOneUser(UserManager<ApplicationUser> userManager,
    string name, string password, string role = null)
        {
            if (userManager.FindByNameAsync(name).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = name, // musi być taki sam jak email, inaczej nie zadziała
                    Email = name
                };
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded && role != null)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            SeedOneUser(userManager, "normaluser@localhost", "nUpass1!", "User");
            SeedOneUser(userManager, "adminuser@localhost", "aUpass1!", "Admin");
        }
    }
}