using DAL;
using DAL.UserModels;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Services
{
    public class ContextConfig
    {
        private static readonly string seedAdminEmail = "admin@gmail.com";
        private static readonly string[] roles = { "Admin", "User" };

        public static async Task SeedDataAsync(ShippingContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            await SeedUserAsync(userManager, roleManager, configuration);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            // 1. Ensure roles exist
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!roleResult.Succeeded)
                        throw new InvalidOperationException(
                            $"Could not create role {role}: " +
                            string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
            }

            // 2. Ensure admin user exists
            var adminUser = await userManager.FindByEmailAsync(seedAdminEmail);
            var seedPassword = configuration["Seed:AdminPassword"];

            if (adminUser == null)
            {
                var adminPassword = seedPassword
                    ?? throw new InvalidOperationException("Seed:AdminPassword is not set.");

                adminUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = seedAdminEmail,
                    Email = seedAdminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Phone = "0000000000"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                    throw new InvalidOperationException(
                        "Could not create admin: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            else if (!string.IsNullOrEmpty(seedPassword) &&
                     !await userManager.CheckPasswordAsync(adminUser, seedPassword))
            {
                // admin already exists: reset its password to the secret
                var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
                var reset = await userManager.ResetPasswordAsync(adminUser, token, seedPassword);
                if (!reset.Succeeded)
                    throw new InvalidOperationException(
                        "Could not reset admin password: " +
                        string.Join(", ", reset.Errors.Select(e => e.Description)));
            }

            // 3. Ensure the admin has the Admin role
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                await userManager.AddToRoleAsync(adminUser, "Admin");
            // 3. Ensure the admin has the Admin role (even if the user already existed)
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}