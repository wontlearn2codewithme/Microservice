using AutoMapper;
using PlatformService.Contracts.Application;
using PlatformService.Contracts.Entities;
using PlatformService.Contracts.Models;
using PlatformService.Contracts.Repositories;
using PlatformService.Contracts.SyncDataServices.Http;

namespace PlatformService.Application.BusinessLogic
{
    public class PlatformManagement : IPlatformManagement
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformManagement(IPlatformRepository platformRepository, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        public async Task<PlatformReadResponse> CreatePlatform(PlatformCreateRequest platformCreateRequest)
        {
            var newPlatform = _mapper.Map<PlatformCreateRequest, Platform>(platformCreateRequest);
            _platformRepository.CreatePlatform(newPlatform);
            _platformRepository.SaveChanges();
            var parsed = _mapper.Map<Platform, PlatformReadResponse>(newPlatform);
            try
            {
                await _commandDataClient.SendPlatformToCommand(parsed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Algo ha ido mal intenando ejecutar SendPlatformToCommand");
                Console.WriteLine(e.ToString());
                throw;
            }
            return parsed;
        }
        public PlatformReadResponse GetPlatformByGuid(Ulid guid)
        {
            var platform = _platformRepository.GetPlatformByGuid(guid);
            ArgumentNullException.ThrowIfNull(platform);
            var platformResponse = _mapper.Map<Platform, PlatformReadResponse>(platform);
            return platformResponse;
        }
        public PlatformReadResponse GetPlatformById(int platformId)
        {
            var platform = _platformRepository.GetPlatformById(platformId);
            ArgumentNullException.ThrowIfNull(platform);
            var platformResponse = _mapper.Map<Platform, PlatformReadResponse>(platform);
            return platformResponse;
        }
        public IEnumerable<PlatformReadResponse> GetAllPlatforms()
        {
            var platforms = _platformRepository.GetAllPlatforms();
            var platformsResponse = _mapper.Map<IEnumerable<Platform>, IEnumerable<PlatformReadResponse>>(platforms);
            return platformsResponse;
        }
        public bool SaveChanges()
        {
            return _platformRepository.SaveChanges();
        }
    }
}
