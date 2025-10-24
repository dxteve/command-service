using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepareDb
    {
        public static void PreparePopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!, isProduction);
            }
        }

        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                System.Console.WriteLine("Atempting to apply migrations...");

                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Could not apply migrations: {ex.Message}");
                    throw;
                }
            }

            if (!context.Platforms.Any())
            {
                System.Console.WriteLine("--> Seeding data...");

                context.Platforms.AddRange(
                    new Platform { Name = "Windows", Publisher = "Microsoft", Cost = "Free" },
                    new Platform { Name = ".NET", Publisher = "Microsoft", Cost = "Free" },
                    new Platform { Name = "Git", Publisher = "Linus Torvalds", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("--> We already have data.");
            }
        }
    }
}