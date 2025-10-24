using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
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
                    System.Console.WriteLine("Event type could not be determined.");
                    break;
            }
        }

        private void AddPlatform(string paltformPublishMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishDto = JsonSerializer.Deserialize<PlatformPublishDto>(paltformPublishMessage);

                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishDto);
    
                    if (!repository.ExternalPlatformExists(platform.ExternalId))
                    {
                        repository.CreatePlatform(platform);
                        repository.SaveChanges();
    
                        System.Console.WriteLine("Platform created.");
                    }
                    else
                        System.Console.WriteLine("Platform already exists.");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Could not create platform: {ex.Message}");                    
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            System.Console.WriteLine("--> Determining event type...");
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType!.Event)
            {
                case "Platform_Published":
                    System.Console.WriteLine("Event type \"Platform_Published\" detected.");
                    return EventType.PlatformPublished;

                default:
                    // System.Console.WriteLine("Event type could not be determined.");
                    return EventType.Undetermined;
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}