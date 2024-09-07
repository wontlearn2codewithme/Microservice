using PlatformService.Contracts.Entities;
using PlatformService.Contracts.Repositories;
using PlatformService.Repository.DatabaseContext;

namespace PlatformService.Repository.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _appDbContext;

        public PlatformRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void CreatePlatform(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);
            _appDbContext.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _appDbContext.Platforms;
        }

        public Platform? GetPlatformById(int id)
        {
            return _appDbContext.Platforms.FirstOrDefault(x => x.Id == id);
        }

        public Platform? GetPlatformByGuid(Ulid ulid)
        {
            return _appDbContext.Platforms.FirstOrDefault(x => x.Ulid == ulid);
        }

        public bool SaveChanges()
        {
            return _appDbContext.SaveChanges() >= 0;
        }
    }
}
