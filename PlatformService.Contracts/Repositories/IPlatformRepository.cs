using PlatformService.Contracts.Entities;

namespace PlatformService.Contracts.Repositories
{
    public interface IPlatformRepository
    {
        public Platform? GetPlatformByGuid(Guid ulid);
        bool SaveChanges();
        List<Platform> GetAllPlatforms();
        Platform? GetPlatformById(int id);
        void CreatePlatform(Platform platform);
    }
}
