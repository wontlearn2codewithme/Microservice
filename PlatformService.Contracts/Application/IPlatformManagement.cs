using PlatformService.Contracts.Models;

namespace PlatformService.Contracts.Application
{
    public interface IPlatformManagement
    {
        Task<PlatformReadResponse> CreatePlatform(PlatformCreateRequest platformCreateRequest);
        List<PlatformReadResponse> GetAllPlatforms();
        PlatformReadResponse GetPlatformByGuid(Guid guid);
        PlatformReadResponse GetPlatformById(int platformId);
        bool SaveChanges();
    }
}
