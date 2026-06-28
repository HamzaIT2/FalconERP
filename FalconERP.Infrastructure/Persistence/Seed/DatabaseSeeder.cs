using FalconERP.Domain.Entities;

namespace FalconERP.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Roles.Any())
        {
            var adminRole = new Role
            {
                Name = "Admin",
                Description = "System Administrator"
            };

            context.Roles.Add(adminRole);

            await context.SaveChangesAsync();

            var adminUser = new User
            {
                FullName = "Administrator",
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                RoleId = adminRole.Id,
                IsActive = true
            };

            context.Users.Add(adminUser);

            await context.SaveChangesAsync();
        }
    }
}