using Microsoft.AspNetCore.Identity;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.Settings;
using RoutineCatalogue.Models.Types;
using RoutineCatalogue.MVC.Data;
using System;
using System.Threading.Tasks;
namespace RoutineCatalogue.MVC.Factories
{
    public class UserSeedFactory
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationSettings appSettings)
        {
            context.Database.EnsureCreated();
            var password = appSettings.AdminPassword;
            var userName = appSettings.AdminUsername;
            foreach (string role in Enum.GetNames(typeof(RoleType)))
                if (await roleManager.FindByNameAsync(role) == null)
                    await roleManager.CreateAsync(new Role(role, $"This is the {role} role.", DateTime.Now));
            if (await userManager.FindByNameAsync(userName) == null)
            {
                var user = new User
                {
                    UserName = userName,
                    Email = userName,
                    Role = await roleManager.FindByNameAsync(RoleType.Admin.ToString())
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, RoleType.Admin.ToString());
                    context.SaveChanges();
                }
            }
        }
    }
}