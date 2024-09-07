

using Microsoft.Extensions.DependencyInjection;
using PlatformService.Contracts.Entities;

namespace PlatformService.Repository.DatabaseContext
{
    public static class PrepDB
    {
        public static void PopulateDb(IServiceScope serviceScope)
        {

            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void SeedData(AppDbContext? appDbContext)
        {
            ArgumentNullException.ThrowIfNull(appDbContext);
            if (appDbContext.Platforms.Any())
            {
                Console.WriteLine("There is already data");
                return;
            }
            Console.WriteLine("creating mock data");
            appDbContext.Platforms.AddRange(
                new Platform
                {
                    Ulid = Ulid.NewUlid(),
                    Cost = "Free",
                    Id = 1,
                    Name = "Name",
                    Publisher = "Publisher"
                },
                new Platform
                {
                    Ulid = Ulid.NewUlid(),
                    Cost = "A lot",
                    Id = 2,
                    Name = "Name2",
                    Publisher = "Publisher2"
                }
            );
            appDbContext.SaveChanges();
        }
    }
}
