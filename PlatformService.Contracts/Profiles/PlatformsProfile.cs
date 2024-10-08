using AutoMapper;
using PlatformService.Contracts.Entities;
using PlatformService.Contracts.Models;

namespace PlatformService.Contracts.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Platform, PlatformReadResponse>();
            CreateMap<PlatformCreateRequest, Platform>();
            CreateMap<Platform, PlatformPublished>();
        }
    }
}
