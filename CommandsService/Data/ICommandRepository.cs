using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();
        IEnumerable<Platform> GetPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command? GetCommandForPlatform(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}
