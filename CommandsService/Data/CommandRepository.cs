using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class CommandRepository(AppDbContext appDbContext) : ICommandRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public void CreateCommand(int platformId, Command command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));
            command.PlatformId = platformId;
            _appDbContext.Commands.Add(command);
        }

        public void CreatePlatform(Platform? platform)
        {
            ArgumentNullException.ThrowIfNull(platform, nameof(platform));
            _appDbContext.Platforms.Add(platform);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _appDbContext.Platforms.Any(x => x.ExternalId == externalPlatformId);
        }

        public Command? GetCommandForPlatform(int platformId, int commandId)
        {
            return _appDbContext.Commands.FirstOrDefault(x => x.PlatformId == platformId && x.Id == commandId);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _appDbContext.Commands.Where(x => x.PlatformId == platformId).OrderBy(x => x.Platform.Name).AsNoTracking();
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _appDbContext.Platforms.AsNoTracking().ToList();
        }

        public bool PlatformExists(int platformId)
        {
            return _appDbContext.Platforms.Any(x => x.Id == platformId);
        }

        public bool SaveChanges()
        {
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
