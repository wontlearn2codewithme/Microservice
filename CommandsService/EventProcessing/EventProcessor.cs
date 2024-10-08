using AutoMapper;
using CommandsService.Data;
using CommandsService.DTOs;
using CommandsService.Models;
using System.Text.Json;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType) 
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string message)
        {
            Console.WriteLine($"--> Determining event for message: {message}");
            var eventType = JsonSerializer.Deserialize<GenericEventDTO>(message);
            if (eventType == null) 
            {
                throw new ArgumentNullException($"Incorrect message: {nameof(message)}");
            }
            switch (eventType.Event) 
            {
                case "Platform_Published":
                    Console.WriteLine("The type is Platform_Published");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("The type is Undetermined");
                    return EventType.Undetermined;
            }
        }
    
        private void AddPlatform(string platformPublishedMessage)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var commandRepository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
            try
            {
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDTO>(platformPublishedMessage);
                var platform = _mapper.Map<Platform>(platformPublishedDto);
                if (!commandRepository.ExternalPlatformExists(platform.ExternalId))
                {
                    commandRepository.CreatePlatform(platform);
                    commandRepository.SaveChanges();
                    Console.WriteLine($"Platform with id {platform.ExternalId} was created successfully!");
                }
                else
                {
                    Console.WriteLine("Platform already exists in command!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong saving the platform in commandservice, exception: {ex}");
                
            }
        }
    }

    public enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
