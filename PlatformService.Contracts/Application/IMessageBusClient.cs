using PlatformService.Contracts.Models;

namespace PlatformService.Contracts.Application
{
    public interface IMessageBusClient
    {
        public void PublishNewPlatform(PlatformPublished platformPublished);
    }
}
