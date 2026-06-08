using Microsoft.AspNetCore.Identity;

namespace DoYouOwnIt.Api.Scripts.Seeds
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                                      RoleManager<IdentityRole> roleManager, ILogger<ApplicationDbContextSeed> logger)
        {
            await context.Database.MigrateAsync();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Moderator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Moderator"));
            }
            if (!await roleManager.RoleExistsAsync("AlphaTester"))
            {
                await roleManager.CreateAsync(new IdentityRole("AlphaTester"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var adminRiley = await userManager.FindByEmailAsync("rileybot@DoYouOwnIt.com");
            if (adminRiley == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "RileyBot",
                    Email = "rileybot@DoYouOwnIt.com",
                    EmailConfirmed = true,
                    DisplayName = "Riley The Border Collie",
                    IsVerified = true
                };

                var result = await userManager.CreateAsync(user, "Arizona@Flagstaff1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    logger.LogInformation("Seeded admin user successfully.");
                }
                else
                {
                    logger.LogError("Failed to seed admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else 
            { 
                await userManager.AddToRoleAsync(adminRiley, "Admin");
                logger.LogInformation("Admin user Riley already exists, ensured Admin role is assigned.");
            }

                var adminAndy = await userManager.FindByEmailAsync("andy@DoYouOwnIt.com");
            if (adminAndy == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "Andy",
                    Email = "andy@DoYouOwnIt.com",
                    EmailConfirmed = true,
                    DisplayName = "Andy",
                    IsVerified = true
                };

                var result = await userManager.CreateAsync(user, "AndysGreatBigPassWord@Nope4");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    logger.LogInformation("Seeded admin user Andy successfully.");
                }
                else
                {
                    logger.LogError("Failed to seed admin user Andy: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                await userManager.AddToRoleAsync(adminAndy, "Admin");
                logger.LogInformation("Admin user Admin already exists, ensured Admin role is assigned.");
            }
        }
    }
}
