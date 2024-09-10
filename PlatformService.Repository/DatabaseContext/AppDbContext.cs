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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Platform>()
                .Property(p => p.Guid)
                .HasConversion(
                ulid => ulid,
                guid => guid);
            modelBuilder
                .Entity<Platform>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
