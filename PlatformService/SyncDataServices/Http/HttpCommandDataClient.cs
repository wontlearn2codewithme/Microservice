using PlatformService.Contracts.Models;
using PlatformService.Contracts.SyncDataServices.Http;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadResponse platformReadResponse)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(platformReadResponse), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandServiceUrl"]}api/Platforms/TestInboundConnection", httpContent);

            if (response.IsSuccessStatusCode) 
            {
                Console.WriteLine("--> Posted to CommandsServices <--");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine("--> Posted to CommandsServices went wrong <--");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
