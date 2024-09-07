using PlatformService.Contracts.Entities;

namespace PlatformService.Contracts.Repositories
{
    public interface IPlatformRepository
    {
        public Platform? GetPlatformByGuid(Ulid ulid);
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        Platform? GetPlatformById(int id);
        void CreatePlatform(Platform platform);
    }
}
