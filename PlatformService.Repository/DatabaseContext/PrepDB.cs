

using Microsoft.Extensions.DependencyInjection;
using PlatformService.Contracts.Entities;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Repository.DatabaseContext
{
    public static class PrepDB
    {
        public static void PopulateDb(IServiceScope serviceScope, bool isProd)
        {
            var appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

            ArgumentNullException.ThrowIfNull(appDbContext);
            if (isProd)
            {
                try
                {
                    Console.WriteLine("Running migrations");
                    appDbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed migrations: {ex}");
                    //throw;
                }

            }
            if (appDbContext.Platforms.Any())
            {
                Console.WriteLine("There is already data");
                return;
            }
            Console.WriteLine("creating mock data");
            appDbContext.Platforms.AddRange(
                new Platform
                {
                    Guid = Guid.NewGuid(),
                    Cost = "Free",
                    Name = "Name",
                    Publisher = "Publisher"
                },
                new Platform
                {
                    Guid = Guid.NewGuid(),
                    Cost = "A lot",
                    Name = "Name2",
                    Publisher = "Publisher2"
                }
            );
            appDbContext.SaveChanges();
        }
    }
}
