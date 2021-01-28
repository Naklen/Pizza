using Pizza.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Pizza.Service;

namespace Pizza
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {            
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            var a = await userManager.FindByNameAsync(Admin.UserName);
            if (a == null)
            {
                User admin = new User { Email = Admin.Email, UserName = Admin.UserName, Address = null, Orders = null };
                IdentityResult result = await userManager.CreateAsync(admin, Admin.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}