using System.Text.Json;
using backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            // var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            // var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            var roles = new List<AppRole>{
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            // users
            var users = new List<AppUser>{
                new AppUser{
                    UserName = "Khan",
                    Country = "Afghanistan",
                    DateOfBirth= DateOnly.FromDateTime(DateTime.Now)
                },
                new AppUser{
                    UserName = "Nick",
                    Country="Denmark",
                    DateOfBirth= DateOnly.FromDateTime(DateTime.Now)        }
    };

            foreach (var user in users)
            {

                user.UserName = user.UserName.ToLower();

                // await userManager.CreateAsync(user, "Password@10");
                await userManager.CreateAsync(user, "Pass@10");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                Country = "Denmark",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
            };

            await userManager.CreateAsync(admin, "Pass@10");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
        }
    }
}