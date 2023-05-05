using Microsoft.AspNetCore.Identity;
using FreshMove.Models.users;
using FreshMove.Constants;
namespace FreshMove.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.Admin));

        }
    }
}
