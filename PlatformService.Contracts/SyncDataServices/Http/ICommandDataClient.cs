using PlatformService.Contracts.Models;

namespace PlatformService.Contracts.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        public Task SendPlatformToCommand(PlatformReadResponse platformReadResponse);
    }
}
