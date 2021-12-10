using System.Linq;
using System.Threading.Tasks;
using WordCounter.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace WordCounter.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var defaultUser = new ApplicationUser { UserName = "raghav", Email = "raghav@test.com" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Raghav@123");
                await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
            }
        }
    }
}
