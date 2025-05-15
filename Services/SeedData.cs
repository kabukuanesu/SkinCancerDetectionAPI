using Microsoft.AspNetCore.Identity;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

namespace SkinCancerDetectionAPI.Services
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

            // Create roles if they don't exist
            string[] roleNames = { "Admin", "Dermatologist", "Doctor", "Nurse", "Patient" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create admin user if it doesn't exist
            var adminEmail = "admin@skincancer.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, "Admin@123");

                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    // Create corresponding User record
                    var admin = new User
                    {
                        FirstName = "System",
                        LastName = "Admin",
                        DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow),
                        Gender = "Male",
                        PhysicalAddress = "123 Admin St",
                        EmailAddress = adminEmail,
                        PhoneNumber = "1234567890",
                        Role = "Admin",
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    };

                    dbContext.ApplicationUsers.Add(admin);
                    await dbContext.SaveChangesAsync();

                    // Create authentication record with hashed password
                    var auth = new Authentication
                    {
                        UserId = admin.UserId,
                        Username = adminEmail,
                        Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        RecoveryQuestion = "Who are we?",
                        RecoveryAnswer = "Skin Cancer"
                    };

                    dbContext.Authentications.Add(auth);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
