using Microsoft.EntityFrameworkCore;
using PlatformService.Contracts.Entities;

namespace PlatformService.Repository.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
    }
}
