using PlatformService.Contracts.Models;

namespace PlatformService.Contracts.Application
{
    public interface IPlatformManagement
    {
        Task<PlatformReadResponse> CreatePlatform(PlatformCreateRequest platformCreateRequest);
        IEnumerable<PlatformReadResponse> GetAllPlatforms();
        PlatformReadResponse GetPlatformByGuid(Ulid guid);
        PlatformReadResponse GetPlatformById(int platformId);
        bool SaveChanges();
    }
}
