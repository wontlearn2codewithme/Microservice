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

        public List<Platform> GetAllPlatforms()
        {
            return _appDbContext.Platforms.ToList();
        }

        public Platform? GetPlatformById(int id)
        {
            return _appDbContext.Platforms.FirstOrDefault(x => x.Id == id);
        }

        public Platform? GetPlatformByGuid(Guid guid)
        {
            return _appDbContext.Platforms.FirstOrDefault(x => x.Guid == guid);
        }

        public bool SaveChanges()
        {
            return _appDbContext.SaveChanges() >= 0;
        }
    }
}
